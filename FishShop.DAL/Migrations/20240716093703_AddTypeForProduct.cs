using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTypeForProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                schema: "other",
                table: "products",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "Тип товара");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                schema: "other",
                table: "products");
        }
    }
}
