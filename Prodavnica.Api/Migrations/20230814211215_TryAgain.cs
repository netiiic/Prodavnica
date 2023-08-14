using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prodavnica.Api.Migrations
{
    public partial class TryAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShoppingItems_ShoppingItemId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShoppingItemId",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "OrederShoppingItem",
                columns: table => new
                {
                    ItemsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrederShoppingItem", x => new { x.ItemsId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrederShoppingItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrederShoppingItem_ShoppingItems_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "ShoppingItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrederShoppingItem_OrderId",
                table: "OrederShoppingItem",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrederShoppingItem");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShoppingItemId",
                table: "Orders",
                column: "ShoppingItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShoppingItems_ShoppingItemId",
                table: "Orders",
                column: "ShoppingItemId",
                principalTable: "ShoppingItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
