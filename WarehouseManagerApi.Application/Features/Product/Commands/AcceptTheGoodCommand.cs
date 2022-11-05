using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagerApi.Domain.Dtos;
using WarehouseManagerApi.Domain.Entities;
using WarehouseManagerApi.Domain.Responses;
using WarehouseManagerApi.Infrastructure.Database;

namespace WarehouseManagerApi.Application.Features.Product;

public class AcceptTheGoodCommand : IRequest<Response<bool>>
{
    public AcceptTheGoodDto AcceptTheGoodDto { get; set; } = new();
}

public class AcceptTheGoodHandler : IRequestHandler<AcceptTheGoodCommand, Response<bool>>
{
    private readonly IWarehouseManagerDbContext _dbContxet;

    public AcceptTheGoodHandler(IWarehouseManagerDbContext dbContxet)
    {
        _dbContxet = dbContxet;
    }

    public async Task<Response<bool>> Handle(AcceptTheGoodCommand request, CancellationToken cancellationToken)
    {


        throw new NotImplementedException();
    }
     
    private async Task<BaseApiResponse> IsProductInDatabase(int idProduct, CancellationToken cancellationToken)
    {
        var product = await _dbContxet.Products
           .FirstOrDefaultAsync(x => x.Id == idProduct, cancellationToken);

        return new BaseApiResponse()
        {
            Success = product != null,
            Message = product != null ? string.Empty : "Podany produkt nie znajduje się w bazie danych!"
        };
    }

    private async Task<Response<int>> FindFreeWarehouseAddressToLocateProduct(
        uint quantity, decimal weight, CancellationToken cancellationToken)
    {

    }

    private async Task<BaseApiResponse> AddWarehouseMovement(AcceptTheGoodCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContxet.Products
            .FirstOrDefaultAsync(x => x.Id == request.AcceptTheGoodDto.IdProduct, cancellationToken);

        if (product == null)
            return new BaseApiResponse("Podany produkt nie znajduje się w bazie danych!");

        var newWarehouseMovement = new WarehouseMovement()
        {

        };
    }

    private async Task<BaseApiResponse> AddWarehouseAddressProduct(AcceptTheGoodCommand request, CancellationToken cancellationToken)
    {

    }
}
