using Microsoft.OpenApi.Models;
using WarehouseManagerApi.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "WarehouseManager API", Version = "v1" }));


#region Add layers

builder.Services.AddInfrastructureLayer(builder.Configuration);

#endregion Add layers

var app = builder.Build();

#region Swagger

app.UseSwaggerUI(c =>
{
    // run automatically swagger when run project
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WarehouseManager API v1");
    c.RoutePrefix = "";
});

app.UseSwagger(x => x.SerializeAsV2 = true);

#endregion Swagger

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();