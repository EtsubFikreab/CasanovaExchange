using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasanovaExchange.Migrations
{
    /// <inheritdoc />
    public partial class Cassanova : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_commodity_Warehouse_CommodityWarehouseId",
                table: "commodity");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Warehouse",
                newName: "WarehouseId");

            migrationBuilder.RenameColumn(
                name: "CommodityWarehouseId",
                table: "commodity",
                newName: "CommodityWarehouseWarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_commodity_CommodityWarehouseId",
                table: "commodity",
                newName: "IX_commodity_CommodityWarehouseWarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_commodity_Warehouse_CommodityWarehouseWarehouseId",
                table: "commodity",
                column: "CommodityWarehouseWarehouseId",
                principalTable: "Warehouse",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_commodity_Warehouse_CommodityWarehouseWarehouseId",
                table: "commodity");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "Warehouse",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CommodityWarehouseWarehouseId",
                table: "commodity",
                newName: "CommodityWarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_commodity_CommodityWarehouseWarehouseId",
                table: "commodity",
                newName: "IX_commodity_CommodityWarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_commodity_Warehouse_CommodityWarehouseId",
                table: "commodity",
                column: "CommodityWarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
