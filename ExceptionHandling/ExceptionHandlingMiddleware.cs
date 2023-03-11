using System.Text;
using System.Text.Json;
using TreeBase.Models;
using TreeBase.Services;

namespace TreeBase.ExceptionHandling
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            ILogger<ExceptionLoggingMiddleware> logger,
            IJournalService repository,
            IdGenerator idGenerator)
        {
            try
            {
                await _next(context);
            }
            catch (SecureException ex)
            {
                var eventId = idGenerator.GenerateInt64Id().ToString();
                logger.LogError(ex, $"SecureException caught. Event ID: {eventId}");

                var requestBody = context.Request.ContentLength.HasValue 
                    ? await GetBody(context.Request) 
                    : string.Empty;

                var exceptionLog = new LogRecord
                {
                    EventId = eventId,
                    Type = "Secure",
                    Timestamp = DateTime.UtcNow,
                    HttpMethod = context.Request.Method,
                    Path = context.Request.Path,
                    Query = JsonSerializer.Serialize(context.Request.Query),
                    Body = requestBody,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace ?? string.Empty,
                };
                await repository.Add(exceptionLog);

                var response = new ErrorResponse
                {
                    Type = "Secure",
                    EventId = eventId,
                    Message = ex.Message,
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                var eventId = idGenerator.GenerateInt64Id().ToString();
                logger.LogError(ex, $"Exception caught. Event ID: {eventId}");

                var exceptionLog = new LogRecord
                {
                    EventId = eventId,
                    Type = nameof(Exception),
                    Timestamp = DateTime.UtcNow,
                    HttpMethod = context.Request.Method,
                    Path = context.Request.Path,
                    Query = JsonSerializer.Serialize(context.Request.Query),
                    Body = await GetBody(context.Request),
                    Message = ex.Message,
                    StackTrace = ex.StackTrace ?? string.Empty,
                };
                await repository.Add(exceptionLog);

                var response = new ErrorResponse
                {
                    Type = nameof(Exception),
                    EventId = eventId,
                    Message = $"Internal server error ID = {eventId}",
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(response);
            }
        }

        private async Task<string> GetBody(HttpRequest request)
        {
            request.EnableBuffering();
            var buffer = new byte[(int)request.ContentLength!];
            await request.Body.ReadAsync(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);
            return Encoding.UTF8.GetString(buffer);
        }
    }
}
