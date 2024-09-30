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
                var requestResource = "";
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
                        ["/tourist/SceneryInfo/fetch"] = new { CaseId = "110", Role = "USER" },
                        ["/tourist/SceneryInfo/getSceneryInfo"] = new { CaseId = "110", Role = "USER" },
                        ["/tourist/SceneryInfo/create"] = new { CaseId = "110", Role = "ADMIN" },
                        ["/tourist/SceneryInfo/update"] = new { CaseId = "110", Role = "ADMIN" },
                        ["/tourist/SceneryInfo/delete"] = new { CaseId = "110", Role = "ADMIN" },
                        ["/tourist/SceneryInfo/search"] = new { CaseId = "110", Role = "ADMIN" },

                        ["/tourist/TouristInfo/fetch"] = new { CaseId = "111", Role = "USER" },
                        ["/tourist/TouristInfo/getTouristInfo"] = new { CaseId = "111", Role = "USER" },
                        ["/tourist/TouristInfo/create"] = new { CaseId = "111", Role = "ADMIN" },
                        ["/tourist/TouristInfo/update"] = new { CaseId = "111", Role = "ADMIN" },
                        ["/tourist/TouristInfo/delete"] = new { CaseId = "111", Role = "ADMIN" },

                        ["/tourist/TouristOrderInfo/fetch"] = new { CaseId = "112", Role = "USER" },
                        ["/tourist/TouristOrderInfo/getTouristOrderInfo"] = new { CaseId = "112", Role = "USER" },
                        ["/tourist/TouristOrderInfo/create"] = new { CaseId = "112", Role = "ADMIN" },
                        ["/tourist/TouristOrderInfo/update"] = new { CaseId = "112", Role = "ADMIN" },
                        ["/tourist/TouristOrderInfo/delete"] = new { CaseId = "112", Role = "ADMIN" },
                        ["/tourist/TouristOrderInfo/search"] = new { CaseId = "110", Role = "ADMIN" },

                        ["/accommodation/booking/book"] = new { CaseId = "210", Role = "USER" },
                        ["/accommodation/booking/getBooking"] = new { CaseId = "210", Role = "USER" },
                        ["/accommodation/booking/delete"] = new { CaseId = "210", Role = "USER" },

                        ["/accommodation/roominfo/fetch"] = new { CaseId = "211", Role = "USER" },
                        ["/accommodation/roominfo/getRoomInfo"] = new { CaseId = "211", Role = "USER" },
                        ["/accommodation/roominfo/create"] = new { CaseId = "211", Role = "ADMIN" },
                        ["/accommodation/roominfo/update"] = new { CaseId = "211", Role = "ADMIN" },
                        ["/accommodation/roominfo/delete"] = new { CaseId = "211", Role = "ADMIN" },

                        ["/weather/weatherInfo"] = new { CaseId = "610", Role = "USER" },

                        ["/outdoor/EquipmentRental/fetch"] = new { CaseId = "310", Role = "USER" },
                        ["/outdoor/EquipmentRental/renter"] = new { CaseId = "310", Role = "USER" },
                        ["/outdoor/EquipmentRental/getEquipmentRental"] = new { CaseId = "310", Role = "USER" },
                        ["/outdoor/EquipmentRental/create"] = new { CaseId = "310", Role = "USER" },
                        ["/outdoor/EquipmentRental/update"] = new { CaseId = "310", Role = "USER" },
                        ["/outdoor/EquipmentRental/delete"] = new { CaseId = "310", Role = "USER" },

                        ["/outdoor/ProductInfo/fetch"] = new { CaseId = "311", Role = "USER" },
                        ["/outdoor/ProductInfo/getProductInfo"] = new { CaseId = "311", Role = "USER" },
                        ["/outdoor/ProductInfo/create"] = new { CaseId = "311", Role = "ADMIN" },
                        ["/outdoor/ProductInfo/update"] = new { CaseId = "311", Role = "ADMIN" },
                        ["/outdoor/ProductInfo/delete"] = new { CaseId = "311", Role = "ADMIN" },

                        ["/outdoor/ProductOrderInfo/fetch"] = new { CaseId = "312", Role = "USER" },
                        ["/outdoor/ProductOrderInfo/getProductOrderInfo"] = new { CaseId = "312", Role = "USER" },
                        ["/outdoor/ProductOrderInfo/buyer"] = new { CaseId = "312", Role = "USER" },
                        ["/outdoor/ProductOrderInfo/create"] = new { CaseId = "312", Role = "USER" },
                        ["/outdoor/ProductOrderInfo/update"] = new { CaseId = "312", Role = "USER" },
                        ["/outdoor/ProductOrderInfo/delete"] = new { CaseId = "312", Role = "USER" },

                        ["/user/User/register"] = new { CaseId = "410", Role = "USER" },
                        ["/user/User/login"] = new { CaseId = "411", Role = "USER" },
                        ["/user/User/rewards"] = new { CaseId = "412", Role = "ADMIN" }
                        // ["/user/User/login"] = new { CaseId = "456", Role = "USER" },
                    };
                var matchedKey = EventLogDetailMap.Keys.FirstOrDefault(k => requestPath.ToString().Contains(k));
                var startTime = DateTime.UtcNow;

                var authorizationHeader = context.Request.Headers["Authorization"].ToString();
                if (!string.IsNullOrWhiteSpace(authorizationHeader))
                {
                    var parts = authorizationHeader.Split(' ');
                    // userId token
                    requestResource = parts[1] + ' ' + parts[2]; 
                }

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
                        Resource = $"{requestResource}",
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
