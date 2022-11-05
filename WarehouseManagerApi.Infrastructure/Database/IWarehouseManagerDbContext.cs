using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagerApi.Domain.Entities;

namespace WarehouseManagerApi.Infrastructure.Database;

public interface IWarehouseManagerDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<WarehouseMovement> WarehouseMovements { get; set; }
    public DbSet<WarehouseAddress> WarehouseAddresses { get; set; }
    public DbSet<WarehouseAddressesProduct> WarehouseAddressesProducts { get; set; }
}
