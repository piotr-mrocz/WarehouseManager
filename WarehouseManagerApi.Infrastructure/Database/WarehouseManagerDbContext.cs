using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagerApi.Infrastructure.Database;
public class WarehouseManagerDbContext : DbContext, IWarehouseManagerDbContext
{
    public WarehouseManagerDbContext(DbContextOptions options) : base(options) { }

    #region DbSet

    #endregion DbSet

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
