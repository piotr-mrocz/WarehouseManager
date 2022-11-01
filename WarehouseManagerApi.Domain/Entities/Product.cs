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
}
