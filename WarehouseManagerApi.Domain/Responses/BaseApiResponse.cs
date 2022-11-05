using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagerApi.Domain.Responses;
public class BaseApiResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }

    public BaseApiResponse() { }

    public BaseApiResponse(string message)
    {
        Success = false;
        Message = message;
    }

    public BaseApiResponse(bool success, string? message = null)
    {
        Success = success;
        Message = message;
    }
}
