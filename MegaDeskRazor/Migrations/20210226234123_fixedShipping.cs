using Microsoft.EntityFrameworkCore.Migrations;

namespace MegaDeskRazor.Migrations
{
    public partial class fixedShipping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingCosts",
                table: "Shipping",
                newName: "ShippingUnder1000");

            migrationBuilder.AddColumn<int>(
                name: "ShippingBtwn10002000",
                table: "Shipping",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShippingOver2000",
                table: "Shipping",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingBtwn10002000",
                table: "Shipping");

            migrationBuilder.DropColumn(
                name: "ShippingOver2000",
                table: "Shipping");

            migrationBuilder.RenameColumn(
                name: "ShippingUnder1000",
                table: "Shipping",
                newName: "ShippingCosts");
        }
    }
}
