using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasanovaExchange.Migrations
{
    /// <inheritdoc />
    public partial class ba_vo05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommodityListing_portfolio_PortfolioId",
                table: "CommodityListing");

            migrationBuilder.DropForeignKey(
                name: "FK_CommodityTransactions_portfolio_PortfolioId",
                table: "CommodityTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_portfolio_AspNetUsers_UserId",
                table: "portfolio");

            migrationBuilder.DropForeignKey(
                name: "FK_portfolio_Wallet_WalletId",
                table: "portfolio");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Commodity_CommodityTradedId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_portfolio",
                table: "portfolio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.RenameTable(
                name: "portfolio",
                newName: "Portfolio");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "CurrentTrade");

            migrationBuilder.RenameIndex(
                name: "IX_portfolio_WalletId",
                table: "Portfolio",
                newName: "IX_Portfolio_WalletId");

            migrationBuilder.RenameIndex(
                name: "IX_portfolio_UserId",
                table: "Portfolio",
                newName: "IX_Portfolio_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_CommodityTradedId",
                table: "CurrentTrade",
                newName: "IX_CurrentTrade_CommodityTradedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Portfolio",
                table: "Portfolio",
                column: "PortfolioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentTrade",
                table: "CurrentTrade",
                column: "TradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommodityListing_Portfolio_PortfolioId",
                table: "CommodityListing",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommodityTransactions_Portfolio_PortfolioId",
                table: "CommodityTransactions",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "PortfolioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentTrade_Commodity_CommodityTradedId",
                table: "CurrentTrade",
                column: "CommodityTradedId",
                principalTable: "Commodity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolio_AspNetUsers_UserId",
                table: "Portfolio",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolio_Wallet_WalletId",
                table: "Portfolio",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "WalletId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommodityListing_Portfolio_PortfolioId",
                table: "CommodityListing");

            migrationBuilder.DropForeignKey(
                name: "FK_CommodityTransactions_Portfolio_PortfolioId",
                table: "CommodityTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentTrade_Commodity_CommodityTradedId",
                table: "CurrentTrade");

            migrationBuilder.DropForeignKey(
                name: "FK_Portfolio_AspNetUsers_UserId",
                table: "Portfolio");

            migrationBuilder.DropForeignKey(
                name: "FK_Portfolio_Wallet_WalletId",
                table: "Portfolio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Portfolio",
                table: "Portfolio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentTrade",
                table: "CurrentTrade");

            migrationBuilder.RenameTable(
                name: "Portfolio",
                newName: "portfolio");

            migrationBuilder.RenameTable(
                name: "CurrentTrade",
                newName: "Transaction");

            migrationBuilder.RenameIndex(
                name: "IX_Portfolio_WalletId",
                table: "portfolio",
                newName: "IX_portfolio_WalletId");

            migrationBuilder.RenameIndex(
                name: "IX_Portfolio_UserId",
                table: "portfolio",
                newName: "IX_portfolio_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentTrade_CommodityTradedId",
                table: "Transaction",
                newName: "IX_Transaction_CommodityTradedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_portfolio",
                table: "portfolio",
                column: "PortfolioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "TradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommodityListing_portfolio_PortfolioId",
                table: "CommodityListing",
                column: "PortfolioId",
                principalTable: "portfolio",
                principalColumn: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommodityTransactions_portfolio_PortfolioId",
                table: "CommodityTransactions",
                column: "PortfolioId",
                principalTable: "portfolio",
                principalColumn: "PortfolioId",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Transaction_Commodity_CommodityTradedId",
                table: "Transaction",
                column: "CommodityTradedId",
                principalTable: "Commodity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
