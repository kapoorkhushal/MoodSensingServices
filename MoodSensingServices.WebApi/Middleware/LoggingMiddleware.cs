using Microsoft.IO;
using System.Text;

namespace MoodSensingServices.WebApi.Middleware
{
    /// <summary>
    /// Middleware to log HttpContext Request-Response content
    /// </summary>
    public class LoggingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _requestDelegate;
        private readonly RecyclableMemoryStreamManager _memoryStreamManager;

        public LoggingMiddleware(RequestDelegate next,
            ILogger<LoggingMiddleware> logger,
            RecyclableMemoryStreamManager memoryStreamManager)
        {
            _logger = logger;
            _requestDelegate = next;
            _memoryStreamManager = memoryStreamManager;
        }

        /// <summary>
        /// Logs request response
        /// </summary>
        /// <param name="context"><see cref="HttpContent"/></param>
        /// <returns>An Asynchronous task</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            // Log the incoming request details (Method, URL, Headers, Body)
            var request = context.Request;
            var requestBody = await ReadRequestBodyAsync(request);

            // Log the incoming request
            _logger.LogInformation(">>> Incoming Request: {Method} {Url} - Headers: {Headers} - Body: {RequestBody}",
                request.Method,
                request.Path,
                request.Headers,
                requestBody);

            // Temporarily replace the response body stream to capture the response
            var originalResponseBodyStream = context.Response.Body;

            using (var responseBodyStream = _memoryStreamManager.GetStream())
            {
                context.Response.Body = responseBodyStream;

                // Call the next middleware in the pipeline
                await _requestDelegate(context);

                // Create copy of stream, because, stream is getting disposed after fetching the response body
                var preservedStream = new MemoryStream();
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                responseBodyStream.CopyTo(preservedStream);
                responseBodyStream.Seek(0, SeekOrigin.Begin);

                // Capture the response details (StatusCode, Body)
                var response = context.Response;
                var responseBody = await ReadResponseBodyAsync(preservedStream);

                // Log the response
                if (context.Response.StatusCode == StatusCodes.Status200OK ||
                    context.Response.StatusCode == StatusCodes.Status201Created ||
                    context.Response.StatusCode == StatusCodes.Status204NoContent)
                {
                    _logger.LogInformation("<<< Outgoing Response: StatusCode {StatusCode} - Body: {ResponseBody}",
                        response.StatusCode,
                        responseBody);
                }
                else
                {
                    _logger.LogError("<<< Outgoing Response: StatusCode {StatusCode} - Body: {ResponseBody}",
                        response.StatusCode,
                        responseBody);
                }

                // Copy the contents of the memory stream to the original response stream
                await responseBodyStream.CopyToAsync(originalResponseBodyStream);
                await responseBodyStream.FlushAsync();
            }
        }

        /// <summary>
        /// read request body
        /// </summary>
        /// <param name="request"><see cref="HttpRequest"/></param>
        /// <returns>returns incoming request body</returns>
        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            // Enable buffering to read the request body stream multiple times
            request.EnableBuffering();
            using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;  // Reset the request body stream position for further processing
            return body;
        }

        /// <summary>
        /// read response body
        /// </summary>
        /// <param name="responseBodyStream"><see cref="MemoryStream"/></param>
        /// <returns>returns outgoing response body</returns>
        private async Task<string> ReadResponseBodyAsync(MemoryStream responseBodyStream)
        {
            // Read the response body content from the memory stream
            responseBodyStream.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(responseBodyStream);
            return await reader.ReadToEndAsync();
        }
    }
}
