using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagerApi.Domain.Dtos;
public class AcceptTheGoodDto
{
    public int IdProduct { get; set; }
    public string? ExtraInfo { get; set; } = null;
    public uint Quantity { get; set; }
    public decimal Weight { get; set; }
}
