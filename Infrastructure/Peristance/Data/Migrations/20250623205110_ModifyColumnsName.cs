using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peristance.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyColumnsName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "Address_street",
                table: "Orders",
                newName: "shipToAddress_street");

            migrationBuilder.RenameColumn(
                name: "Address_lastname",
                table: "Orders",
                newName: "shipToAddress_lastname");

            migrationBuilder.RenameColumn(
                name: "Address_firstname",
                table: "Orders",
                newName: "shipToAddress_firstname");

            migrationBuilder.RenameColumn(
                name: "Address_country",
                table: "Orders",
                newName: "shipToAddress_country");

            migrationBuilder.RenameColumn(
                name: "Address_city",
                table: "Orders",
                newName: "shipToAddress_city");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Orders",
                newName: "BuyerEmail");

            migrationBuilder.RenameColumn(
                name: "OrderStatus",
                table: "Orders",
                newName: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "shipToAddress_street",
                table: "Orders",
                newName: "Address_street");

            migrationBuilder.RenameColumn(
                name: "shipToAddress_lastname",
                table: "Orders",
                newName: "Address_lastname");

            migrationBuilder.RenameColumn(
                name: "shipToAddress_firstname",
                table: "Orders",
                newName: "Address_firstname");

            migrationBuilder.RenameColumn(
                name: "shipToAddress_country",
                table: "Orders",
                newName: "Address_country");

            migrationBuilder.RenameColumn(
                name: "shipToAddress_city",
                table: "Orders",
                newName: "Address_city");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "OrderStatus");

            migrationBuilder.RenameColumn(
                name: "BuyerEmail",
                table: "Orders",
                newName: "UserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
