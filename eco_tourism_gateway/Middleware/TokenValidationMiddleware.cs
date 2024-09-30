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
using MySql.Data.MySqlClient;

namespace eco_tourism_gateway.Middleware {
        public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _connectionString;

        // Define paths that do not require token validation
        private static readonly string[] _publicPaths = { 
            "/user/api/User/register", 
            "/user/api/User/login", 
            "tourist/SceneryInfo/fetch",
            "/swagger",               // Swagger UI base path
            "/swagger/",              // Swagger UI sub-paths (if necessary)
            "/swagger/v1/swagger.json" // Swagger JSON endpoint
            };

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
            _connectionString = "server=localhost;database=eco_tourism;user=root;password=root";
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"Request Path: {context.Request.Path}");
            // Check if the request path requires token validation
            var isNeedVaild = IsPublicPath(context.Request.Path);
            if (isNeedVaild)
            {
                await _next(context); // Skip token validation
                return;
            }

            // Retrieve the token from the Authorization header
            var authorizationHeader = context.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authorizationHeader) && !isNeedVaild)
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
            var isValid = await ValidateTokenFromDatabaseAsync(userId, token);

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
            // Decode the URL in case it's encoded
            string pathString = Uri.UnescapeDataString(path.ToString().Trim());
            Console.WriteLine($"Decoded Request Path: {pathString}");
            
            // Check if the request path is in the list of public paths
            foreach (var publicPath in _publicPaths)
            {
                if (pathString.StartsWith(publicPath, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private async Task<bool> ValidateTokenFromDatabaseAsync(int userId, string token)
        {
            var query = "SELECT COUNT(1) FROM `eco_tourism_user_userinfo` WHERE Id = @UserId AND Token = @Token";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@Token", token);

                        // execute
                        var result = await command.ExecuteScalarAsync();

                        // Convert the result to long to avoid conversion errors
                        return Convert.ToInt64(result) > 0; // If the result is greater than 0, the token is valid
                    }
                }
            }
            catch (Exception ex)
            {
                // throw error
                Console.WriteLine($"Error validating token: {ex.Message}");
                return false;
            }
        }
    }

}
