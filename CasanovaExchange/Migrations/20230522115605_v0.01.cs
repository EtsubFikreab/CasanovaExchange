using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasanovaExchange.Migrations
{
    /// <inheritdoc />
    public partial class v001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommodityTransaction_Commodity_CommodityId",
                table: "CommodityTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_CommodityTransaction_Portfolio_PortfolioId",
                table: "CommodityTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommodityTransaction",
                table: "CommodityTransaction");

            migrationBuilder.RenameTable(
                name: "CommodityTransaction",
                newName: "CommodityTransactions");

            migrationBuilder.RenameIndex(
                name: "IX_CommodityTransaction_PortfolioId",
                table: "CommodityTransactions",
                newName: "IX_CommodityTransactions_PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_CommodityTransaction_CommodityId",
                table: "CommodityTransactions",
                newName: "IX_CommodityTransactions_CommodityId");

            migrationBuilder.AlterColumn<int>(
                name: "PortfolioId",
                table: "CommodityTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommodityTransactions",
                table: "CommodityTransactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommodityTransactions_Commodity_CommodityId",
                table: "CommodityTransactions",
                column: "CommodityId",
                principalTable: "Commodity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommodityTransactions_Portfolio_PortfolioId",
                table: "CommodityTransactions",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "PortfolioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommodityTransactions_Commodity_CommodityId",
                table: "CommodityTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_CommodityTransactions_Portfolio_PortfolioId",
                table: "CommodityTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommodityTransactions",
                table: "CommodityTransactions");

            migrationBuilder.RenameTable(
                name: "CommodityTransactions",
                newName: "CommodityTransaction");

            migrationBuilder.RenameIndex(
                name: "IX_CommodityTransactions_PortfolioId",
                table: "CommodityTransaction",
                newName: "IX_CommodityTransaction_PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_CommodityTransactions_CommodityId",
                table: "CommodityTransaction",
                newName: "IX_CommodityTransaction_CommodityId");

            migrationBuilder.AlterColumn<int>(
                name: "PortfolioId",
                table: "CommodityTransaction",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommodityTransaction",
                table: "CommodityTransaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommodityTransaction_Commodity_CommodityId",
                table: "CommodityTransaction",
                column: "CommodityId",
                principalTable: "Commodity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommodityTransaction_Portfolio_PortfolioId",
                table: "CommodityTransaction",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "PortfolioId");
        }
    }
}
