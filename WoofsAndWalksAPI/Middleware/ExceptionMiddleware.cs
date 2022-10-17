using System.Net;
using System.Text.Json;
using WoofsAndWalksAPI.Errors;

namespace WoofsAndWalksAPI.Middleware;

public class ExceptionMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ExceptionMiddleware> _logger;
	private readonly IHostEnvironment _env;

	public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
	{
		_next = next;
		_logger = logger;
		_env = env;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, ex.Message);
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			var response = _env.IsDevelopment()
				? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) // prevent exceptions in the exception handler by avoiding null
				: new ApiException(context.Response.StatusCode, "Internal Server Error");	
			
			var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }; // ensure json is return in CamelCase

			var json = JsonSerializer.Serialize(response, options);

			await context.Response.WriteAsync(json);
		}
	}
}
