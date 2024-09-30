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
                        ["/tourist/SceneryInfo/fetch"] = new { CaseId = "110", TaskId = "001", Role = "USER" },
                        ["/tourist/SceneryInfo/getSceneryInfo"] = new { CaseId = "110", TaskId = "002", Role = "USER" },
                        ["/tourist/SceneryInfo/create"] = new { CaseId = "110", TaskId = "003", Role = "ADMIN" },
                        ["/tourist/SceneryInfo/update"] = new { CaseId = "110", TaskId = "004", Role = "ADMIN" },
                        ["/tourist/SceneryInfo/delete"] = new { CaseId = "110", TaskId = "006", Role = "ADMIN" },
                        ["/tourist/SceneryInfo/search"] = new { CaseId = "110", TaskId = "007", Role = "ADMIN" },

                        ["/tourist/TouristInfo/fetch"] = new { CaseId = "111", TaskId = "008", Role = "USER" },
                        ["/tourist/TouristInfo/getTouristInfo"] = new { CaseId = "111", TaskId = "009", Role = "USER" },
                        ["/tourist/TouristInfo/create"] = new { CaseId = "111", TaskId = "010", Role = "ADMIN" },
                        ["/tourist/TouristInfo/update"] = new { CaseId = "111", TaskId = "011", Role = "ADMIN" },
                        ["/tourist/TouristInfo/delete"] = new { CaseId = "111", TaskId = "012", Role = "ADMIN" },

                        ["/tourist/TouristOrderInfo/fetch"] = new { CaseId = "112", TaskId = "013", Role = "USER" },
                        ["/tourist/TouristOrderInfo/getTouristOrderInfo"] = new { CaseId = "112", TaskId = "014", Role = "USER" },
                        ["/tourist/TouristOrderInfo/create"] = new { CaseId = "112", TaskId = "015", Role = "ADMIN" },
                        ["/tourist/TouristOrderInfo/update"] = new { CaseId = "112", TaskId = "016", Role = "ADMIN" },
                        ["/tourist/TouristOrderInfo/delete"] = new { CaseId = "112", TaskId = "017", Role = "ADMIN" },
                        ["/tourist/TouristOrderInfo/search"] = new { CaseId = "110", TaskId = "018", Role = "ADMIN" },

                        ["/accommodation/booking/book"] = new { CaseId = "210", TaskId = "019", Role = "USER" },
                        ["/accommodation/booking/getBooking"] = new { CaseId = "210", TaskId = "020", Role = "USER" },
                        ["/accommodation/booking/delete"] = new { CaseId = "210", TaskId = "021", Role = "USER" },

                        ["/accommodation/roominfo/fetch"] = new { CaseId = "211", TaskId = "022", Role = "USER" },
                        ["/accommodation/roominfo/getRoomInfo"] = new { CaseId = "211", TaskId = "023", Role = "USER" },
                        ["/accommodation/roominfo/create"] = new { CaseId = "211", TaskId = "024", Role = "ADMIN" },
                        ["/accommodation/roominfo/update"] = new { CaseId = "211", TaskId = "025", Role = "ADMIN" },
                        ["/accommodation/roominfo/delete"] = new { CaseId = "211", TaskId = "026", Role = "ADMIN" },

                        ["/weather/weatherInfo"] = new { CaseId = "610", TaskId = "044", Role = "USER" },

                        ["/outdoor/EquipmentRental/fetch"] = new { CaseId = "310", TaskId = "027", Role = "USER" },
                        ["/outdoor/EquipmentRental/renter"] = new { CaseId = "310", TaskId = "028", Role = "USER" },
                        ["/outdoor/EquipmentRental/getEquipmentRental"] = new { CaseId = "310", TaskId = "029", Role = "USER" },
                        ["/outdoor/EquipmentRental/create"] = new { CaseId = "310", TaskId = "030", Role = "USER" },
                        ["/outdoor/EquipmentRental/update"] = new { CaseId = "310", TaskId = "031", Role = "USER" },
                        ["/outdoor/EquipmentRental/delete"] = new { CaseId = "310", TaskId = "032", Role = "USER" },

                        ["/outdoor/ProductInfo/fetch"] = new { CaseId = "311", TaskId = "033", Role = "USER" },
                        ["/outdoor/ProductInfo/getProductInfo"] = new { CaseId = "311", TaskId = "034", Role = "USER" },
                        ["/outdoor/ProductInfo/create"] = new { CaseId = "311", TaskId = "035", Role = "ADMIN" },
                        ["/outdoor/ProductInfo/update"] = new { CaseId = "311", TaskId = "036", Role = "ADMIN" },
                        ["/outdoor/ProductInfo/delete"] = new { CaseId = "311", TaskId = "037", Role = "ADMIN" },

                        ["/outdoor/ProductOrderInfo/fetch"] = new { CaseId = "312", TaskId = "038", Role = "USER" },
                        ["/outdoor/ProductOrderInfo/getProductOrderInfo"] = new { CaseId = "312", TaskId = "039", Role = "USER" },
                        ["/outdoor/ProductOrderInfo/buyer"] = new { CaseId = "312", TaskId = "040", Role = "USER" },
                        ["/outdoor/ProductOrderInfo/create"] = new { CaseId = "312", TaskId = "041", Role = "USER" },
                        ["/outdoor/ProductOrderInfo/update"] = new { CaseId = "312", TaskId = "042", Role = "USER" },
                        ["/outdoor/ProductOrderInfo/delete"] = new { CaseId = "312", TaskId = "043", Role = "USER" },

                        ["/user/User/register"] = new { CaseId = "410", TaskId = "044", Role = "USER" },
                        ["/user/User/login"] = new { CaseId = "411", TaskId = "045", Role = "USER" },
                        ["/user/User/rewards"] = new { CaseId = "412", TaskId = "046", Role = "ADMIN" }
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
                        TaskId = detailValues.TaskId,
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
