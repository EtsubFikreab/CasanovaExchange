using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasanovaExchange.Migrations
{
    /// <inheritdoc />
    public partial class v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCommodity_Commodity_CommoditiesId",
                table: "UserCommodity");

            migrationBuilder.RenameColumn(
                name: "CommoditiesId",
                table: "UserCommodity",
                newName: "CommodityId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCommodity_CommoditiesId",
                table: "UserCommodity",
                newName: "IX_UserCommodity_CommodityId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommodity_Commodity_CommodityId",
                table: "UserCommodity",
                column: "CommodityId",
                principalTable: "Commodity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCommodity_Commodity_CommodityId",
                table: "UserCommodity");

            migrationBuilder.RenameColumn(
                name: "CommodityId",
                table: "UserCommodity",
                newName: "CommoditiesId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCommodity_CommodityId",
                table: "UserCommodity",
                newName: "IX_UserCommodity_CommoditiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommodity_Commodity_CommoditiesId",
                table: "UserCommodity",
                column: "CommoditiesId",
                principalTable: "Commodity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
