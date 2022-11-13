using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagerApi.Domain.Consts;
using WarehouseManagerApi.Domain.Dtos;
using WarehouseManagerApi.Domain.Entities;
using WarehouseManagerApi.Domain.Enums;
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
        BaseApiResponse isProductInDatabaseResponse = await IsProductInDatabase(request.AcceptTheGoodDto.IdProduct, cancellationToken);

        if (!isProductInDatabaseResponse.Success)
            return new Response<bool>(isProductInDatabaseResponse.Message);

        var warehouseAddressResponse = await FindFreeWarehouseAddressToLocateProduct(
            request.AcceptTheGoodDto.Quantity, request.AcceptTheGoodDto.Weight, cancellationToken);

        if (!warehouseAddressResponse.Success)
            return new Response<bool>(warehouseAddressResponse.Message);

        var addWarehouseMovementResponse = await AddWarehouseMovement(
            request, warehouseAddressResponse.Data, cancellationToken);

        if (!addWarehouseMovementResponse.Success)
            return new Response<bool>(addWarehouseMovementResponse.Message);

        var addWarehouseAddressProductResponse = await AddWarehouseAddressProduct(
            request, warehouseAddressResponse.Data, cancellationToken);

        return new Response<bool>()
        {
            Success = addWarehouseAddressProductResponse.Success,
            Message = addWarehouseAddressProductResponse.Message,
            Data = addWarehouseAddressProductResponse.Success
        };
    }
     
    private async Task<BaseApiResponse> IsProductInDatabase(int idProduct, CancellationToken cancellationToken)
    {
        var product = await _dbContxet.Products
           .FirstOrDefaultAsync(x => x.Id == idProduct, cancellationToken);

        return new BaseApiResponse()
        {
            Success = product != null,
            Message = product != null 
                ? "Produkt znajduje się w bazie danych"
                : "Podany produkt nie znajduje się w bazie danych!"
        };
    } 

    private async Task<Response<int>> FindFreeWarehouseAddressToLocateProduct(
        uint quantity, decimal weight, CancellationToken cancellationToken)
    {
        var address = await _dbContxet.WarehouseAddresses
            .FirstOrDefaultAsync(x => x.MaxLoad <= weight && x.MaxNumberOfPallets == quantity, cancellationToken);

        if (address == null)
            return new Response<int>("Brak wolnego miejsca na magazynie!");

        return new Response<int>()
        {
            Success = true,
            Data = address.Id
        };
    }

    private async Task<BaseApiResponse> AddWarehouseMovement(
        AcceptTheGoodCommand request, int idWarehouseAddress, CancellationToken cancellationToken)
    {
        var newWarehouseMovement = new WarehouseMovement()
        {
            IdProduct = request.AcceptTheGoodDto.IdProduct,
            Quantity = request.AcceptTheGoodDto.Quantity,
            Weight = request.AcceptTheGoodDto.Weight,
            ExtraInfo = request.AcceptTheGoodDto.ExtraInfo,
            ToWarehouseAddressId = idWarehouseAddress,
            MovementType = (int)WarehouseMovementsEnum.AcceptTheGood
        };

        await _dbContxet.WarehouseMovements.AddAsync(newWarehouseMovement);
        var result = (await _dbContxet.SaveChangesAsync(cancellationToken).ConfigureAwait(false)) > 0;

        return new BaseApiResponse()
        {
            Success = result,
            Message = result 
                    ? "Poprawnie dodano ruch magazynowy" 
                    : "Wystąpił błąd podczas dodawania ruchu magazynowego"
        };
    }

    private async Task<BaseApiResponse> AddWarehouseAddressProduct(
        AcceptTheGoodCommand request, int idWarehouseAddress, CancellationToken cancellationToken)
    {
        var newWarehouseAddressProduct = new WarehouseAddressesProduct()
        {
            IdProduct = request.AcceptTheGoodDto.IdProduct,
            IdWarehouseAddress = idWarehouseAddress,
            QuantityAvailable = request.AcceptTheGoodDto.Quantity,
            QuantityReservation = default,
            Weight = request.AcceptTheGoodDto.Weight
        };

        await _dbContxet.WarehouseAddressesProducts.AddAsync(newWarehouseAddressProduct);
        var result = (await _dbContxet.SaveChangesAsync(cancellationToken).ConfigureAwait(false)) > 0;

        return new BaseApiResponse()
        {
            Success = result,
            Message = result
                    ? ReturnMessagesConst.ReturnSucceededMessage
                    : "Wystąpił błąd podczas dodawania produktu na stan"
        };
    }
}
