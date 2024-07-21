using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedHashInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "hash",
                schema: "secure",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "Хеш пароля");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hash",
                schema: "secure",
                table: "users");
        }
    }
}
