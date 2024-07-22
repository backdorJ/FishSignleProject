using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserRegisterStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                schema: "secure",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "Статус регистрации");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                schema: "secure",
                table: "users");
        }
    }
}
