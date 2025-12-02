using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Edit_Site_Banner_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColSize",
                table: "SiteBanners");

            migrationBuilder.RenameColumn(
                name: "BannersLocations",
                table: "SiteBanners",
                newName: "Placement");

            migrationBuilder.AddColumn<int>(
                name: "GridColumnSize",
                table: "SiteBanners",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GridColumnSize",
                table: "SiteBanners");

            migrationBuilder.RenameColumn(
                name: "Placement",
                table: "SiteBanners",
                newName: "BannersLocations");

            migrationBuilder.AddColumn<string>(
                name: "ColSize",
                table: "SiteBanners",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");
        }
    }
}
