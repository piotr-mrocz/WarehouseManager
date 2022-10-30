using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WarehouseManagerApi.Infrastructure.Database;

namespace WarehouseManagerApi.Application.Extensions;
public static class LayerExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        var assembly = AppDomain.CurrentDomain.GetAssemblies();

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<IWarehouseManagerDbContext, WarehouseManagerDbContext>();
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
