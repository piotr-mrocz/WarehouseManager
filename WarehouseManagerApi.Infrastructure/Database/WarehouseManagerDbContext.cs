﻿using Microsoft.EntityFrameworkCore;
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
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<WarehouseMovement> WarehouseMovements { get; set; } = null!;
    public DbSet<WarehouseAddress> WarehouseAddresses { get; set; } = null!;

    #endregion DbSet

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>(user =>
        {
            user.HasOne(u => u.Role)
           .WithMany(r => r.Users)
           .HasForeignKey(x => x.IdRole);
        });

        builder.Entity<WarehouseMovement>(movement =>
        {
            movement.HasOne(m => m.Product)
            .WithMany(p => p.WarehouseMovements)
            .HasForeignKey(x => x.IdProduct);
        });

        base.OnModelCreating(builder);
    }
}
