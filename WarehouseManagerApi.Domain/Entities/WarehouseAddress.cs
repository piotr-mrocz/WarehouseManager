using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagerApi.Domain.Entities;
public class WarehouseAddress
{
    public int Id { get; set; }
    public string Address { get; set; } = null!;
    public decimal MaxLoad { get; set; }
    public int MaxNumberOfPallets { get; set; }
    public bool IsFull { get; set; }
    public bool IsActive { get; set; }
}
