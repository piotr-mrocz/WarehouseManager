using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseManagerApi.Infrastructure.Migrations
{
    public partial class AddWarehouseAddressesProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FromWarehouseAddressId",
                table: "WarehouseMovements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToWarehouseAddressId",
                table: "WarehouseMovements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnePieceWeight",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "WarehouseAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxLoad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxNumberOfPallets = table.Column<int>(type: "int", nullable: false),
                    IsFull = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseAddressesProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdWarehouseAddress = table.Column<int>(type: "int", nullable: false),
                    IdProduct = table.Column<int>(type: "int", nullable: false),
                    QuantityAvailable = table.Column<long>(type: "bigint", nullable: false),
                    QuantityReservation = table.Column<long>(type: "bigint", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseAddressesProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseAddressesProducts_Products_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseAddressesProducts_WarehouseAddresses_IdWarehouseAddress",
                        column: x => x.IdWarehouseAddress,
                        principalTable: "WarehouseAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseMovements_FromWarehouseAddressId",
                table: "WarehouseMovements",
                column: "FromWarehouseAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseMovements_ToWarehouseAddressId",
                table: "WarehouseMovements",
                column: "ToWarehouseAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseAddressesProducts_IdProduct",
                table: "WarehouseAddressesProducts",
                column: "IdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseAddressesProducts_IdWarehouseAddress",
                table: "WarehouseAddressesProducts",
                column: "IdWarehouseAddress");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseMovements_WarehouseAddresses_FromWarehouseAddressId",
                table: "WarehouseMovements",
                column: "FromWarehouseAddressId",
                principalTable: "WarehouseAddresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseMovements_WarehouseAddresses_ToWarehouseAddressId",
                table: "WarehouseMovements",
                column: "ToWarehouseAddressId",
                principalTable: "WarehouseAddresses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseMovements_WarehouseAddresses_FromWarehouseAddressId",
                table: "WarehouseMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseMovements_WarehouseAddresses_ToWarehouseAddressId",
                table: "WarehouseMovements");

            migrationBuilder.DropTable(
                name: "WarehouseAddressesProducts");

            migrationBuilder.DropTable(
                name: "WarehouseAddresses");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseMovements_FromWarehouseAddressId",
                table: "WarehouseMovements");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseMovements_ToWarehouseAddressId",
                table: "WarehouseMovements");

            migrationBuilder.DropColumn(
                name: "FromWarehouseAddressId",
                table: "WarehouseMovements");

            migrationBuilder.DropColumn(
                name: "ToWarehouseAddressId",
                table: "WarehouseMovements");

            migrationBuilder.DropColumn(
                name: "OnePieceWeight",
                table: "Products");
        }
    }
}
