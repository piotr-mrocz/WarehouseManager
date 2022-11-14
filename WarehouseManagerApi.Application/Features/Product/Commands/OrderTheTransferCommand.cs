using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagerApi.Domain.Consts;
using WarehouseManagerApi.Domain.Entities;
using WarehouseManagerApi.Domain.Enums;
using WarehouseManagerApi.Domain.Responses;
using WarehouseManagerApi.Infrastructure.Database;

namespace WarehouseManagerApi.Application.Features.Product;
public class OrderTheTransferCommand : IRequest<BaseApiResponse>
{
    public int IdProduct { get; set; }
    public int Quantity { get; set; }
    public string? ExtraInfo { get; set; }
}

public class OrderTheTransferHandler : IRequestHandler<OrderTheTransferCommand, BaseApiResponse>
{
    private readonly IWarehouseManagerDbContext _dbContxet;

    public OrderTheTransferHandler(IWarehouseManagerDbContext dbContxet)
    {
        _dbContxet = dbContxet;
    }

    public async Task<BaseApiResponse> Handle(OrderTheTransferCommand request, CancellationToken cancellationToken)
    {
        var productAddress = await GetProduct(request, cancellationToken);

        if (productAddress == null)
            return new BaseApiResponse("Nie odnaleziono wystarczającej ilości produktu w bazie danych!");

        var findAnotherFreeAddress = await FindFreeWarehouseAddressToLocateProduct(
            quantity: request.Quantity,
            weight: productAddress.Product.OnePieceWeight * request.Quantity,
            actualAddress: productAddress.IdWarehouseAddress,
            cancellationToken: cancellationToken);

        if (!findAnotherFreeAddress.Success)
            return new BaseApiResponse(findAnotherFreeAddress.Message);

        var addMovementResponse = await AddWarehouseMovement(
            request,
            idFromAddress: productAddress.IdWarehouseAddress,
            idToAddress: findAnotherFreeAddress.Data,
            weight: productAddress.Product.OnePieceWeight * request.Quantity,
            cancellationToken);

        if (!addMovementResponse.Success)
            return new BaseApiResponse(addMovementResponse.Message);

        return await ChangeAddress(
             idFromAddress: productAddress.IdWarehouseAddress,
            idToAddress: findAnotherFreeAddress.Data,
            quantity: request.Quantity,
            idProduct: request.IdProduct,
            weightOnePiece: productAddress.Product.OnePieceWeight,
            cancellationToken);
    }

    private async Task<WarehouseAddressesProduct?> GetProduct(OrderTheTransferCommand request, CancellationToken cancellationToken)
    {
        var warehouseAddressProduct = await _dbContxet.WarehouseAddressesProducts
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.IdProduct == request.IdProduct && x.QuantityAvailable <= request.Quantity, cancellationToken);

        return warehouseAddressProduct;
    }

    private async Task<BaseApiResponse> AddWarehouseMovement(
        OrderTheTransferCommand request,
        int idFromAddress,
        int idToAddress,
        decimal weight,
        CancellationToken cancellationToken)
    {
        var newWarehouseMovement = new WarehouseMovement()
        {
            IdProduct = request.IdProduct,
            Quantity = (uint)request.Quantity,
            Weight = weight,
            ExtraInfo = request.ExtraInfo,
            FromWarehouseAddressId = idFromAddress,
            ToWarehouseAddressId = idToAddress,
            MovementType = (int)WarehouseMovementsEnum.Displacement
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

    private async Task<Response<int>> FindFreeWarehouseAddressToLocateProduct(
        int quantity, decimal weight, int actualAddress, CancellationToken cancellationToken)
    {
        var address = await _dbContxet.WarehouseAddresses
            .FirstOrDefaultAsync(x =>
                    x.MaxLoad <= weight &&
                    x.MaxNumberOfPallets == quantity &&
                    x.Id != actualAddress,
                    cancellationToken);

        if (address == null)
            return new Response<int>("Brak wolnego miejsca na magazynie!");

        return new Response<int>()
        {
            Success = true,
            Data = address.Id
        };
    }

    private async Task<BaseApiResponse> ChangeAddress(
        int idFromAddress,
        int idToAddress,
        int quantity,
        int idProduct,
        decimal weightOnePiece,
        CancellationToken cancellationToken)
    {
        var productAddress = await _dbContxet.WarehouseAddressesProducts
            .FirstOrDefaultAsync(x => x.IdProduct == idProduct && x.IdWarehouseAddress == idFromAddress, cancellationToken);

        if (productAddress == null)
            return new BaseApiResponse("Nie odnaleziono produktu z lokalizacji");

        if (productAddress.QuantityAvailable == quantity)
        {
            ChangeAddress(productAddress, idToAddress, cancellationToken);

            return new BaseApiResponse(true, ReturnMessagesConst.ReturnSucceededMessage);
        }

        AddNewAddressProduct(idProduct, idToAddress, quantity, weightOnePiece * quantity, cancellationToken);

        return await UpdateWarehouseAddressesProduct(productAddress, quantity, weightOnePiece, cancellationToken);
    }

    private async Task<BaseApiResponse> UpdateWarehouseAddressesProduct(
        WarehouseAddressesProduct productAddress,
        int quantity,
        decimal weightOnePiece,
        CancellationToken cancellationToken)
    {
        productAddress.QuantityAvailable -= (uint)quantity;
        productAddress.Weight -= (quantity * weightOnePiece);

        _dbContxet.WarehouseAddressesProducts.Update(productAddress);
        var result = (await _dbContxet.SaveChangesAsync(cancellationToken).ConfigureAwait(false)) > 0;

        return new BaseApiResponse()
        {
            Success = result,
            Message = result
                    ? ReturnMessagesConst.ReturnSucceededMessage
                    : "Wystąpił błąd podczas aktualizowania stanu magazynowego"
        };
    }

    private void AddNewAddressProduct(
        int idProduct,
        int idToAddress,
        int quantity,
        decimal weight,
        CancellationToken cancellationToken)
    {
        var newAddressProduct = new WarehouseAddressesProduct()
        {
            IdProduct = idProduct,
            IdWarehouseAddress = idToAddress,
            QuantityAvailable = (uint)quantity,
            QuantityReservation = default,
            Weight = weight
        };

        _dbContxet.WarehouseAddressesProducts.Add(newAddressProduct);
        _dbContxet.SaveChangesAsync(cancellationToken);
    }

    private void ChangeAddress(
        WarehouseAddressesProduct productAddress, int idToAddress, CancellationToken cancellationToken)
    {
        productAddress.IdWarehouseAddress = idToAddress;

        _dbContxet.WarehouseAddressesProducts.Update(productAddress);
        _dbContxet.SaveChangesAsync(cancellationToken);
    }
}
