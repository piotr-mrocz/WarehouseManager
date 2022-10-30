using System.Net;
using System.Text.Json;
using WarehouseManagerApi.Domain.Exceptions;
using WarehouseManagerApi.Domain.Responses;

namespace WarehouseManagerApi.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            await HandleException(error, context);
        }
    }

    private async Task HandleException(Exception error, HttpContext context)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var responseModel = new Response<string>(error.Message);

        switch (error)
        {
            case ApiException e:
                // custom application error
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case KeyNotFoundException e:
                // not found error
                response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            default:
                // unhandled error
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var result = JsonSerializer.Serialize(responseModel);

        await response.WriteAsync(result);
    }
}
