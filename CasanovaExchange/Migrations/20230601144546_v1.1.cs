using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasanovaExchange.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCommodity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortfolioId = table.Column<int>(type: "int", nullable: false),
                    CommoditiesId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCommodity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCommodity_Commodity_CommoditiesId",
                        column: x => x.CommoditiesId,
                        principalTable: "Commodity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCommodity_Portfolio_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCommodity_CommoditiesId",
                table: "UserCommodity",
                column: "CommoditiesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCommodity_PortfolioId",
                table: "UserCommodity",
                column: "PortfolioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCommodity");
        }
    }
}
