using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "other");

            migrationBuilder.EnsureSchema(
                name: "secure");

            migrationBuilder.CreateTable(
                name: "products",
                schema: "other",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Наименование товара"),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания"),
                    update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "secure",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false, comment: "Логин"),
                    email = table.Column<string>(type: "text", nullable: false, comment: "Почта"),
                    first_name = table.Column<string>(type: "text", nullable: false, comment: "Имя"),
                    last_name = table.Column<string>(type: "text", nullable: false, comment: "Фамилия"),
                    patronymic = table.Column<string>(type: "text", nullable: true, comment: "Отчество"),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "День рождение пользователя"),
                    create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания"),
                    update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products",
                schema: "other");

            migrationBuilder.DropTable(
                name: "users",
                schema: "secure");
        }
    }
}
