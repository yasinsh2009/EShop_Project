using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Add_ProductCategory_Entity_and_Change_BaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "editorName");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Tickets",
                newName: "editorName");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "TicketMessages",
                newName: "editorName");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Sliders",
                newName: "editorName");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "SiteSettings",
                newName: "editorName");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "SiteBanners",
                newName: "editorName");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Roles",
                newName: "editorName");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Questions",
                newName: "editorName");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Products",
                newName: "editorName");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Features",
                newName: "editorName");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Contacts",
                newName: "editorName");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "AboutUs",
                newName: "editorName");

            migrationBuilder.CreateTable(
                name: "productCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UrlName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    editorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productCategories_productCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "productCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "productSelectedCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    editorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productSelectedCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productSelectedCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_productSelectedCategories_productCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "productCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productCategories_ParentId",
                table: "productCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_productSelectedCategories_ProductCategoryId",
                table: "productSelectedCategories",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_productSelectedCategories_ProductId",
                table: "productSelectedCategories",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productSelectedCategories");

            migrationBuilder.DropTable(
                name: "productCategories");

            migrationBuilder.RenameColumn(
                name: "editorName",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "editorName",
                table: "Tickets",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "editorName",
                table: "TicketMessages",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "editorName",
                table: "Sliders",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "editorName",
                table: "SiteSettings",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "editorName",
                table: "SiteBanners",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "editorName",
                table: "Roles",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "editorName",
                table: "Questions",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "editorName",
                table: "Products",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "editorName",
                table: "Features",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "editorName",
                table: "Contacts",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "editorName",
                table: "AboutUs",
                newName: "UserName");
        }
    }
}
