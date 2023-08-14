using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prodavnica.Api.Migrations
{
    public partial class FixMaybe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingItems_Orders_OrderId",
                table: "ShoppingItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingItems_OrderId",
                table: "ShoppingItems");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ShoppingItems");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShoppingItems_ShoppingItemId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShoppingItemId",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "ShoppingItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItems_OrderId",
                table: "ShoppingItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingItems_Orders_OrderId",
                table: "ShoppingItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
