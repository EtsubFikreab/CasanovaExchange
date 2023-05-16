using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasanovaExchange.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trade_Wallet_WalletId",
                table: "trade");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_trade_PortfolioId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trade",
                table: "trade");

            migrationBuilder.RenameTable(
                name: "trade",
                newName: "portfolio");

            migrationBuilder.RenameIndex(
                name: "IX_trade_WalletId",
                table: "portfolio",
                newName: "IX_portfolio_WalletId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "portfolio",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_portfolio",
                table: "portfolio",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_portfolio_UserId",
                table: "portfolio",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_portfolio_AspNetUsers_UserId",
                table: "portfolio",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_portfolio_Wallet_WalletId",
                table: "portfolio",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "WalletId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_portfolio_PortfolioId",
                table: "Transaction",
                column: "PortfolioId",
                principalTable: "portfolio",
                principalColumn: "PortfolioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_portfolio_AspNetUsers_UserId",
                table: "portfolio");

            migrationBuilder.DropForeignKey(
                name: "FK_portfolio_Wallet_WalletId",
                table: "portfolio");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_portfolio_PortfolioId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_portfolio",
                table: "portfolio");

            migrationBuilder.DropIndex(
                name: "IX_portfolio_UserId",
                table: "portfolio");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "portfolio");

            migrationBuilder.RenameTable(
                name: "portfolio",
                newName: "trade");

            migrationBuilder.RenameIndex(
                name: "IX_portfolio_WalletId",
                table: "trade",
                newName: "IX_trade_WalletId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_trade",
                table: "trade",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_trade_Wallet_WalletId",
                table: "trade",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "WalletId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_trade_PortfolioId",
                table: "Transaction",
                column: "PortfolioId",
                principalTable: "trade",
                principalColumn: "PortfolioId");
        }
    }
}
