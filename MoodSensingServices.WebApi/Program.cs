using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoodSensingServices.Application.Extensions;
using MoodSensingServices.Application.Interfaces;
using MoodSensingServices.Domain.Settings;
using MoodSensingServices.Infrastructure;
using MoodSensingServices.Infrastructure.Context;
using Newtonsoft.Json;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Polly.Wrap;
using System.Diagnostics;
using System.Text;

public static class Program
{

    private const string AllowSpecificOrigins = "AllowSpecificOrigins";
    private const string ResiliencePolicy = "ResiliencePolicy";
    public static IConfiguration? Configuration { get; private set; }

    /// <summary>
    /// The main entry to the application
    /// </summary>
    /// <param name="args">array of arguements</param>
    /// <returns>Representing asynchro</returns>
    public static async Task Main(string[] args)
    {
        Activity.DefaultIdFormat = ActivityIdFormat.W3C;
        await CreateApplicationBuilder(args).RunAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// initialize the application to configure pipeline & routes.
    /// </summary>
    /// <param name="args"></param>
    /// <returns>builder of type <see cref="WebApplicationBuilder"/></returns>
    public static WebApplication CreateApplicationBuilder(string[] args)
    {
        // initialize the webApplication builder instance.
        var builder = WebApplication.CreateSlimBuilder(new WebApplicationOptions()
        {
            Args = args
        });

        // added configurable json file
        builder.Configuration.AddJsonFile("/opt/app-config/appSettings.json", optional: true, reloadOnChange: true);

        // web host configuration
        builder.WebHost
            .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
            .UseKestrelCore()
            .ConfigureKestrel(options =>
            {
                options.Configure(builder.Configuration.GetSection("Kestrel"));
                options.AddServerHeader = false;
            })
            .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
            .UseShutdownTimeout(TimeSpan.FromSeconds(10));

        Configuration = builder.Configuration;

        ConfigureServices(builder.Services, builder.Environment);

        var app = builder.Build();

        // generates service of type IApiVersionDescriptionProvider
        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        // add service collection to the newly created startup instance.
        Configure(app, apiVersionDescriptionProvider);

        return app;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    private static void ConfigureServices(IServiceCollection services, IWebHostEnvironment environment)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));
        services.AddApplicationServices();
        ConfigureOptions(services);
        ConfigureAuthentication(services);
        ConfigureBasicServices(services);
        ConfigureDatabase(services);
        AddTransientFailurePolicies(services);
        ConfigureSwaggerOptions(services);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    private static void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        app.UseSwagger();
        app.UseSwaggerUI(setupAction =>
        {
            foreach (var apiVersionDescription in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                setupAction.SwaggerEndpoint($"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                    $"{apiVersionDescription.GroupName}");
                setupAction.RoutePrefix = "";
            }
        });

        // TODO: Add logging middleware
        //app.UseMiddleware<LoggingMiddleware>();
        app.UseRouting();
        app.UseCors(AllowSpecificOrigins);
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseStatusCodePages();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/health");
            endpoints.MapControllers();
        });
    }

    private static void ConfigureOptions(IServiceCollection services)
    {
        services.AddOptions();
        services.Configure<PolicyServiceSettings>(Configuration?.GetSection("PolicyServiceSettings")!);
        services.Configure<ApplicationSettings>(Configuration?.GetSection("ApplicationSettings")!);
    }

    private static void ConfigureAuthentication(IServiceCollection services)
    {
        var authSettings = Configuration?.GetSection("AuthorizationSettings").Get<AuthorizationSettings>();

        //// Add JWT Authentication service
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = authSettings?.ValidIssuer,
                ValidAudience = authSettings?.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings?.SecretKey!))
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    // Log the error
                    Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorization(); // Add authorization services
    }

    /// <summary>
    /// Adds Policy, scope handler & common business service components
    /// </summary>
    /// <param name="services"></param>
    private static void ConfigureBasicServices(IServiceCollection services)
    {
        services.AddHealthChecks();
        services.AddHttpContextAccessor();
        services.AddLocalization(options => options.ResourcesPath = "Resources");

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConsole();
        });

        // Add CORS
        var corsAllowedOrigins = Configuration?.GetSection("ApplicationSettings:CorsAllowedOrigins").GetChildren().Select(asc => asc.Value).ToArray() ?? [];

        services.AddCors(options =>
        {
            options.AddPolicy(AllowSpecificOrigins,
            builder =>
            {
                builder.WithOrigins(corsAllowedOrigins as string[])
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetPreflightMaxAge(TimeSpan.FromSeconds(30));
            });
        });

        services
            .AddControllers()
            .AddNewtonsoftJson(options => options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Unspecified);
        // Add common application services
        services.AddLogging();
    }

    /// <summary>
    /// configure database context
    /// </summary>
    /// <param name="services"></param>
    private static void ConfigureDatabase(IServiceCollection services)
    {
        string connectionString = Configuration["ConnectionString"];

        services.AddDbContext<MSAContext>(options => options.UseSqlServer(connectionString).UseLazyLoadingProxies(), ServiceLifetime.Scoped);
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }

    /// <summary>
    /// Adds default transient HttpResponseMessage error retry and timeout policies to <see cref="PolicyRegistry"/> 
    /// <remark> The policy uses <see cref="PolicyBuilder"/> to configure the retries for Http 400 and 5xX Status Codes. </remark>
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> 
    private static void AddTransientFailurePolicies(IServiceCollection services)
    {
        var policySettings = Configuration?.GetSection("PolicyServiceSettings").Get<PolicyServiceSettings>();
        var delay = Backoff.ExponentialBackoff(initialDelay: TimeSpan.FromMilliseconds(policySettings!.BackOffDelayInMilliseconds.GetValueOrDefault()), retryCount: policySettings.RetryCount.GetValueOrDefault(3), fastFirst: false);
        var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(delay);
        var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(policySettings.TimeoutInSeconds.GetValueOrDefault(1), Polly.Timeout.TimeoutStrategy.Pessimistic);

        AsyncPolicyWrap<HttpResponseMessage> responseWrapper = Policy.WrapAsync(retryPolicy, timeoutPolicy);

        var registry = services.AddPolicyRegistry();
        registry.Add(ResiliencePolicy, responseWrapper);
        services.AddSingleton(registry);
    }

    /// <summary>
    /// circuit breaker policy
    /// </summary>
    /// <returns></returns>
    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(60));
    }

    private static void ConfigureSwaggerOptions(IServiceCollection services)
    {
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddApiVersioning(service =>
        {
            service.DefaultApiVersion = new ApiVersion(1, 0);
            service.AssumeDefaultVersionWhenUnspecified = true;
            service.ReportApiVersions = true;
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Mood Sensing services",
            });
        });
    }
}
