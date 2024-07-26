using JwtToken.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Net;

namespace JwtToken.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKey = "ApiKey";
        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKey, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key was not provided ");
                return;
            }
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(ApiKey);
            if (!ValidateApiKeyForMaster(extractedApiKey, context))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }
            await _next(context);
        }

        public bool ValidateApiKeyForMaster(string apiKey, HttpContext context)
        {
            try
            {
                bool hasApiKey = false;

                var param = new SqlParameter[]
                {
                new SqlParameter()
                {
                    ParameterName = "@Apikey",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = apiKey,
                },
                };

                using (SqlConnection conn = new SqlConnection())
                {
                    var ConnectionStrings = context.RequestServices.GetRequiredService<IConfiguration>();
                    var webConfigConnectionString = ConnectionStrings.GetValue<string>("ConnectionStrings:dbcs");
                    conn.ConnectionString = webConfigConnectionString;
                    conn.Open();
                    var command = conn.CreateCommand();
                    command.CommandText = "EXEC " + "usp_ValidateApiKey @Apikey";

                    foreach (var parameterDefinition in param)
                    {
                        command.Parameters.Add(new SqlParameter(parameterDefinition.ParameterName, parameterDefinition.Value));
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hasApiKey = reader.GetBoolean(reader.GetOrdinal("message"));
                        }
                    }
                    conn.Close();
                    return hasApiKey;
                }
            }

            catch (Exception)
            {

                throw;
            }

        }
    }

}
