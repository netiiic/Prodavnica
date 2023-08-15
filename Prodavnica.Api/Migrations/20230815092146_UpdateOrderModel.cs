using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prodavnica.Api.Migrations
{
    public partial class UpdateOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Finalized",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finalized",
                table: "Orders");
        }
    }
}
