namespace MoodSensingServices.Domain.Settings
{
    public class ApplicationSettings
    {
        public List<string>? CorsAllowedOrigins {  get; set; }

        public string? FileDirectory { get; set; }
    }
}
