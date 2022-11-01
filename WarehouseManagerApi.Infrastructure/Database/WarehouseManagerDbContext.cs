using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagerApi.Domain.Entities;

namespace WarehouseManagerApi.Infrastructure.Database;
public class WarehouseManagerDbContext : DbContext, IWarehouseManagerDbContext
{
    public WarehouseManagerDbContext(DbContextOptions<WarehouseManagerDbContext> options) : base(options) { }

    #region DbSet

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;

    #endregion DbSet

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>(user =>
        {
            user.HasOne(u => u.Role)
           .WithMany(r => r.Users)
           .HasForeignKey(x => x.IdRole);
        });

        base.OnModelCreating(builder);
    }
}
