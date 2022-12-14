using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagerApi.Domain.Entities;

namespace WarehouseManagerApi.Infrastructure.Database;
public class DatabaseSeeder
{
    private readonly WarehouseManagerDbContext _dbContext;

    public DatabaseSeeder(WarehouseManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        if (!_dbContext.Database.CanConnect())
            return;

        if (!_dbContext.Products.Any())
            GenerateSomeProducts();

        if (!_dbContext.WarehouseAddresses.Any())
            GenerateWarehouseAddresses();
    }

    private void GenerateSomeProducts()
    {
        var products = new List<Product>()
        { 
            new Product()
            {
                EasyName = "Product 1",
                FullName = "Product 1 with some additional information",
                QuantityOnFullPallet = 60,
                QuantityAvailable = default(int)
            },

            new Product()
            {
                EasyName = "Product 2",
                FullName = "Product 2 with some additional information",
                QuantityOnFullPallet = 50,
                QuantityAvailable = default(int)
            },

            new Product()
            {
                EasyName = "Product 3",
                FullName = "Product 3 with some additional information",
                QuantityOnFullPallet = 16,
                QuantityAvailable = default(int)
            },

            new Product()
            {
                EasyName = "Product 4",
                FullName = "Product 4 with some additional information",
                QuantityOnFullPallet = 12,
                QuantityAvailable = default(int)
            },

            new Product()
            {
                EasyName = "Product 5",
                FullName = "Product 5 with some additional information",
                QuantityOnFullPallet = 45,
                QuantityAvailable = default(int)
            },

            new Product()
            {
                EasyName = "Product 6",
                FullName = "Product 6 with some additional information",
                QuantityOnFullPallet = 60,
                QuantityAvailable = default(int)
            }
        };

        _dbContext.Products.AddRange(products);
        _dbContext.SaveChanges();
    }

    private void GenerateWarehouseAddresses()
    {
        var addresses = new List<WarehouseAddress>()
        {
            new WarehouseAddress()
            {
                Address = "Address 1",
                MaxLoad = 250m,
                MaxNumberOfPallets = 5,
                IsFull = false,
                IsActive = true
            },

            new WarehouseAddress()
            {
                Address = "Address 2",
                MaxLoad = 200m,
                MaxNumberOfPallets = 2,
                IsFull = false,
                IsActive = true
            },

            new WarehouseAddress()
            {
                Address = "Address 3",
                MaxLoad = 250m,
                MaxNumberOfPallets = 3,
                IsFull = false,
                IsActive = true
            },

            new WarehouseAddress()
            {
                Address = "Address 4",
                MaxLoad = 250m,
                MaxNumberOfPallets = 5,
                IsFull = false,
                IsActive = false
            },

            new WarehouseAddress()
            {
                Address = "Address 5",
                MaxLoad = 250m,
                MaxNumberOfPallets = 3,
                IsFull = false,
                IsActive = true
            },

            new WarehouseAddress()
            {
                Address = "Address 6",
                MaxLoad = 200m,
                MaxNumberOfPallets = 2,
                IsFull = false,
                IsActive = true
            }
        };

        _dbContext.WarehouseAddresses.AddRange(addresses);
        _dbContext.SaveChanges();
    }
}
