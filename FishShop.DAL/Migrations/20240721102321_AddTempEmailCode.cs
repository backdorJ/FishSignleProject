using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTempEmailCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "temp_email_code",
                schema: "secure",
                table: "users",
                type: "text",
                nullable: true,
                comment: "Код подтверждения для почты");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "temp_email_code",
                schema: "secure",
                table: "users");
        }
    }
}
