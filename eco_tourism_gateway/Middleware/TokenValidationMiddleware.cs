// namespace eco_tourism_gateway.Middleware
// {
//     public class TokenValidationMiddleware
//     {
//         private readonly RequestDelegate _next;
//         private readonly HttpClient _httpClient;

//         public TokenValidationMiddleware(RequestDelegate next, HttpClient httpClient)
//         {
//             _next = next;
//             _httpClient = httpClient;
//         }

//         public async Task InvokeAsync(HttpContext context)
//         {
//             var path = context.Request.Path.Value ?? string.Empty;;
//             Console.WriteLine($"Request Path: {path}");
//             // jump validation
//             if (path.StartsWith("/public") || path.StartsWith("/auth"))
//             {
//                 // public
//                 await _next(context);
//                 return;
//             }

//             var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
//             if (string.IsNullOrEmpty(token))
//             {
//                 context.Response.StatusCode = 401; // Unauthorized
//                 return;
//             }

//             var userId = 1; // id
//             var userServiceResponse = await _httpClient.GetAsync($"http://user-service/validate-token?userId={userId}&token={token}");

//             if (!userServiceResponse.IsSuccessStatusCode)
//             {
//                 context.Response.StatusCode = 401; // Unauthorized
//                 return;
//             }

//             await _next(context);
//         }
//     }
// }

using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace eco_tourism_gateway.Middleware {
        public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpClient _httpClient;

        // Define paths that do not require token validation
        private static readonly string[] _publicPaths = { 
            "/api/User/register", 
            "/api/User/login", 
            "/swagger",               // Swagger UI base path
            "/swagger/",              // Swagger UI sub-paths (if necessary)
            "/swagger/v1/swagger.json" // Swagger JSON endpoint
            
            };

        public TokenValidationMiddleware(RequestDelegate next, HttpClient httpClient)
        {
            _next = next;
            _httpClient = httpClient;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the request path requires token validation
            if (IsPublicPath(context.Request.Path))
            {
                await _next(context); // Skip token validation
                return;
            }

            // Retrieve the token from the Authorization header
            var authorizationHeader = context.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authorizationHeader))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Unauthorized
                await context.Response.WriteAsync("Token is missing");
                return;
            }

            // Split the token into parts
            var parts = authorizationHeader.Split(' ');

            if (parts.Length != 3 || parts[0] != "Bearer")
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Unauthorized
                await context.Response.WriteAsync("Invalid token format");
                return;
            }

            // Extract userId and token
            if (!int.TryParse(parts[1], out var userId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest; // Bad Request
                await context.Response.WriteAsync("Invalid user ID");
                return;
            }

            var token = parts[2];

            // Call the user service's validation endpoint
            var isValid = await ValidateTokenAsync(userId, token);

            if (!isValid)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Token is invalid
                await context.Response.WriteAsync("Token is invalid or expired");
                return;
            }

            await _next(context); // Continue processing the request
        }

        private bool IsPublicPath(PathString path)
        {
            // Check if the request path is in the list of public paths
            foreach (var publicPath in _publicPaths)
            {
                if (path.StartsWithSegments(publicPath, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private async Task<bool> ValidateTokenAsync(int userId, string token)
        {
            // Call the user service's validation API
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:8091/api/user/{userId}/validate-token?token={token}");

            var response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode; // Return whether the validation was successful
        }
    }

}
