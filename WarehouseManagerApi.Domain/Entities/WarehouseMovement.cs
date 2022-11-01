using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagerApi.Domain.Entities;
public class WarehouseMovement
{
    public int Id { get; set; }
    public int IdProduct { get; set; }
    public int MovementType { get; set; }
    public string? ExtraInfo { get; set; } = null;
    public uint Quantity { get; set; }

    public Product Product { get; set; } = null!;
}
