using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagerApi.Domain.Entities;
public class WarehouseAddressesProduct
{
    public int Id { get; set; }
    public int IdWarehouseAddress { get; set; }
    public int IdProduct { get; set; }
    public uint QuantityAvailable { get; set; }
    public uint QuantityReservation { get; set; }
    public decimal Weight { get; set; }

    public WarehouseAddress WarehouseAddress { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
