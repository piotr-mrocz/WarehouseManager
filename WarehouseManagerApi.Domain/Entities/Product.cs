using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagerApi.Domain.Entities;
public class Product
{
    public int Id { get; set; }
    public string EasyName { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public uint QuantityOnFullPallet { get; set; }
    public uint QuantityAvailable { get; set; }
    public decimal OnePieceWeight { get; set; }

    public ICollection<WarehouseAddressesProduct> WarehouseAddressesProducts { get; set; } = new List<WarehouseAddressesProduct>();
    public ICollection<WarehouseMovement> WarehouseMovements { get; set; } = new List<WarehouseMovement>();
}
