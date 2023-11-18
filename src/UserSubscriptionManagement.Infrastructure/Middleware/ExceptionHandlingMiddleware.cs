
using System.Net;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using UserSubscriptionManagement.Contracts.Exceptions;

namespace UserSubscriptionManagement.Infrastructure.Middleware;

/// <summary>
///     The exception handling middleware class
/// </summary>
public sealed class ExceptionHandlingMiddleware
{
    /// <summary>
    ///     The next
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ExceptionHandlingMiddleware" /> class
    /// </summary>
    /// <param name="next">The next</param>
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    ///     Invokes the context
    /// </summary>
    /// <param name="context">The context</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }


    /// <summary>
    ///     Handles the exception using the specified context
    /// </summary>
    /// <param name="context">The context</param>
    /// <param name="exception">The exception</param>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        var jsonResponse = JsonSerializer.Serialize(new ApiException(exception.Message, context.Response.StatusCode));
        return context.Response.WriteAsync(jsonResponse);
    }
}