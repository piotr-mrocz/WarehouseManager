using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagerApi.Infrastructure.Database;
using WarehouseManagerApi.Infrastructure.Settings;

namespace WarehouseManagerApi.Infrastructure.Extensions;
public static class LayerExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WarehouseManagerDbContext>(x => x.UseSqlServer(configuration.GetConnectionString(nameof(ConnectionString.WarehouseManager))));
    }
}
