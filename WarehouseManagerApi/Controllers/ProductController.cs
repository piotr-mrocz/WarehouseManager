using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagerApi.Application.Features.Product;

namespace WarehouseManagerApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AcceptTheGood(AcceptTheGoodCommand request)
        => Ok(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> OrderTheTransfer(OrderTheTransferCommand request)
         => Ok(await _mediator.Send(request));
}
