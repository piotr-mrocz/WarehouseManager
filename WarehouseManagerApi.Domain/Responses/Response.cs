using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagerApi.Domain.Responses;
public class Response<T> : BaseApiResponse
{
    public T? Data { get; set; }

	public Response() : base() { }

	public Response(string message)
	{
		Success = false;
		Message = message;
	}

	public Response(T data, string? message = null)
	{
		Success = true;
		Message = message;
		Data = data;
	}
}
