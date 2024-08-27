using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Api
{
    public class RequestTime
    {
        private readonly RequestDelegate requestDelegate;
        private readonly ILogger<RequestTime> logger;
        public IConfiguration configuration;
        public RequestTime(RequestDelegate _requestDelegate, ILogger<RequestTime> _logger, IConfiguration _configuration)
        {
            requestDelegate = _requestDelegate;
            logger = _logger;
            configuration = _configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            await requestDelegate(context);
            stopwatch.Stop();

            var logMessage = $"Request: {context.Request.Method} {context.Request.Path} respuesta en {stopwatch.ElapsedMilliseconds} ms";
            logger.LogInformation(logMessage);

            await File.AppendAllTextAsync(string.Format("{0}{1}", configuration["RutaLogTiempo"], configuration["ArchivoLogTiempo"]), logMessage + Environment.NewLine);
        }
    }
}
