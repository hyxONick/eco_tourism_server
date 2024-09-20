using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using eco_tourism_gateway.DB;
using Microsoft.AspNetCore.Identity;
namespace eco_tourism_gateway.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public LoggingMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EcoEventLogContext>();

                // Capture request information
                var requestPath = context.Request.Path;
                var requestMethod = context.Request.Method;

                // Header
                // var headers = context.Request.Headers;
                // foreach (var header in headers)
                // {
                //     Console.WriteLine($"{header.Key}: {header.Value}");
                // }
                
                // Capture request body
                // string? requestBody = null;
                // using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
                // {
                //     requestBody = await reader.ReadToEndAsync();
                //     context.Request.Body.Position = 0; // Reset the stream position
                // }

                // query param
                // var queryString = context.Request.QueryString.ToString();
                // var routeData = context.GetRouteData();
                // var routeValues = routeData.Values;
                // Console.WriteLine($"Query String: {queryString}");
                // foreach (var key in routeValues.Keys)
                // {
                //     Console.WriteLine($"Route Data - {key}: {routeValues[key]}");
                // }

                // User identify
                // var user = context.User;
                // var userIdentity = user.Identity;
                // var userName = userIdentity?.Name;
                // Console.WriteLine($"User Name: {userName}");
                
                // Host Address
                // var scheme = context.Request.Scheme;
                // var host = context.Request.Host;

                // Console.WriteLine($"Scheme: {scheme}");
                // Console.WriteLine($"Host: {host}");

                var EventLogDetailMap = new Dictionary<string, object>
                    {
                        ["/weather/weatherInfo"] = new { CaseId = "456", Resource = "TOKEN", Role = "USER" }
                    };
                var matchedKey = EventLogDetailMap.Keys.FirstOrDefault(k => requestPath.ToString().Contains(k));
                var startTime = DateTime.UtcNow;

                // do next middleware
                await _next(context);
                
                // Create log record
                EventLog? logEntry = null;
                if (matchedKey != null && EventLogDetailMap.TryGetValue(matchedKey, out var details))
                {
                    var detailValues = (dynamic)details;  // Use dynamic to access anonymous type properties
                    Console.WriteLine($"matchedKey: {matchedKey}");
                    // Create log entry only when a match is found
                    logEntry = new EventLog
                    {
                        TaskId = Guid.NewGuid().ToString(), // Generate unique TaskId
                        StartTimestamp = startTime,
                        EndTimestamp = DateTime.UtcNow,
                        Resource = $"{requestMethod} {requestPath} {detailValues.Resource}",
                        CaseId = detailValues.CaseId,
                        Role = detailValues.Role
                    };
                    dbContext.EventLogs.Add(logEntry);
                    await dbContext.SaveChangesAsync();
                }
                // Create log record
                // var logEntry = new EventLog
                // {
                //     CaseId = null, // map case id
                //     TaskId = Guid.NewGuid().ToString(), // Generate unique TaskId
                //     Timestamp = DateTime.UtcNow,
                //     Resource = $"{requestMethod} {requestPath}",
                //     Role = null
                // };
                // Save logs to the database
            }
        }
    }
}
