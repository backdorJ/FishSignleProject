using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesAndPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания"),
                    update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleName = table.Column<string>(type: "text", nullable: false),
                    create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания"),
                    update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles_permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: true),
                    PermissionId = table.Column<Guid>(type: "uuid", nullable: true),
                    create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания"),
                    update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles_permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_roles_permissions_permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "permissions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_roles_permissions_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_roles_permissions_PermissionId",
                table: "roles_permissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_roles_permissions_RoleId",
                table: "roles_permissions",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "roles_permissions");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
