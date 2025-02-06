using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class LogService
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LogService> _logger;
    private readonly string _logFilePath;

    public LogService(RequestDelegate next, ILogger<LogService> logger)
    {
        _next = next;
        _logger = logger;
        _logFilePath = $"log_{DateTime.UtcNow:yyyy-MM-dd}.txt";
    }

    public async Task Invoke(HttpContext context)
    {
        var requestTime = DateTime.UtcNow;
        var ip = context.Connection.RemoteIpAddress?.ToString();
        var requestMethod = context.Request.Method;
        var requestPath = context.Request.Path;

        context.Request.EnableBuffering();
        string requestBody = "";
        if (context.Request.ContentLength > 0)
        {
            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            requestBody = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0; 
        }

        await _next(context);

        var responseTime = DateTime.UtcNow;
        var statusCode = context.Response.StatusCode;

        string logEntry = $"\n[{requestTime:HH:mm:ss}] IP: {ip}, \n{requestMethod} {requestPath} -> {statusCode}, " +
                          $"\nTime: {responseTime - requestTime} ms, \nRequestBody: {requestBody}";

        _logger.LogInformation(logEntry);

        await File.AppendAllTextAsync(_logFilePath, logEntry + Environment.NewLine);
    }
}
