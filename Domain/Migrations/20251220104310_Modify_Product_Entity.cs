using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Modify_Product_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDiscountUses_Products_ProductId",
                table: "ProductDiscountUses");

            migrationBuilder.DropIndex(
                name: "IX_ProductDiscountUses_ProductId",
                table: "ProductDiscountUses");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductDiscountUses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "ProductDiscountUses",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscountUses_ProductId",
                table: "ProductDiscountUses",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDiscountUses_Products_ProductId",
                table: "ProductDiscountUses",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
