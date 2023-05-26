using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasanovaExchange.Migrations
{
    /// <inheritdoc />
    public partial class v002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commodity_Portfolio_PortfolioId",
                table: "Commodity");

            migrationBuilder.DropIndex(
                name: "IX_Commodity_PortfolioId",
                table: "Commodity");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "Commodity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PortfolioId",
                table: "Commodity",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Commodity_PortfolioId",
                table: "Commodity",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commodity_Portfolio_PortfolioId",
                table: "Commodity",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "PortfolioId");
        }
    }
}
