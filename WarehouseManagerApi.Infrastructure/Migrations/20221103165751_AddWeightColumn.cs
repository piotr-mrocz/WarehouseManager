using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseManagerApi.Infrastructure.Migrations
{
    public partial class AddWeightColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "WarehouseMovements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "WarehouseMovements");
        }
    }
}
