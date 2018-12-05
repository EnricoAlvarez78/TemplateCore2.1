using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AccessControl");

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "AccessControl",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    Status = table.Column<bool>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "AccessControl",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    Password = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 120, nullable: false),
                    Status = table.Column<bool>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "AccessControl",
                columns: table => new
                {
                    IdUser = table.Column<Guid>(nullable: false),
                    IdRole = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.IdUser, x.IdRole });
                    table.ForeignKey(
                        name: "FK_UserRole_Role",
                        column: x => x.IdRole,
                        principalSchema: "AccessControl",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User",
                        column: x => x.IdUser,
                        principalSchema: "AccessControl",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_IdRole",
                schema: "AccessControl",
                table: "UserRole",
                column: "IdRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "AccessControl");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "AccessControl");

            migrationBuilder.DropTable(
                name: "User",
                schema: "AccessControl");
        }
    }
}
