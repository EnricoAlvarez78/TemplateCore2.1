using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AccessControl");

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "AccessControl",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(unicode: false, maxLength: 64, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 512, nullable: false),
                    Status = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "AccessControl",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(unicode: false, maxLength: 64, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 512, nullable: false),
                    Status = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    CreationDate = table.Column<DateTime>(nullable: false)
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
                    Status = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                schema: "AccessControl",
                columns: table => new
                {
                    IdRole = table.Column<Guid>(nullable: false),
                    IdPermission = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.IdRole, x.IdPermission });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission",
                        column: x => x.IdPermission,
                        principalSchema: "AccessControl",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role",
                        column: x => x.IdRole,
                        principalSchema: "AccessControl",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.InsertData(
                schema: "AccessControl",
                table: "Permission",
                columns: new[] { "Id", "CreationDate", "Description", "Name", "Status" },
                values: new object[,]
                {
                    { new Guid("048e322a-dc47-4094-b4d0-acb594e0abb6"), new DateTime(2018, 12, 6, 16, 28, 37, 134, DateTimeKind.Local), "Create new User", "AddUser", true },
                    { new Guid("51e3b2ef-7ab1-4abf-9c48-868d7a6be851"), new DateTime(2018, 12, 6, 16, 28, 37, 135, DateTimeKind.Local), "Edit User", "EditUser", true },
                    { new Guid("717aa7a4-b58d-440a-a8f9-b20ba17bbf5d"), new DateTime(2018, 12, 6, 16, 28, 37, 135, DateTimeKind.Local), "Remove User", "RemoveUser", true },
                    { new Guid("fb7a64d7-99db-4966-b16e-f2a1f1f9d8aa"), new DateTime(2018, 12, 6, 16, 28, 37, 135, DateTimeKind.Local), "List User", "ListUser", true },
                    { new Guid("3e409dc4-2b30-49c2-ac0d-49ff0a7eca75"), new DateTime(2018, 12, 6, 16, 28, 37, 135, DateTimeKind.Local), "Create new Role", "AddRole", true },
                    { new Guid("57b5f483-65ca-4e40-b4ee-f40b70eea058"), new DateTime(2018, 12, 6, 16, 28, 37, 135, DateTimeKind.Local), "Edit Role", "EditRole", true },
                    { new Guid("96336698-2a7e-48bc-ba37-259f0c213872"), new DateTime(2018, 12, 6, 16, 28, 37, 135, DateTimeKind.Local), "Remove Role", "RemoveRole", true },
                    { new Guid("6a817d7d-c494-4f5c-afaa-0799f7d1621c"), new DateTime(2018, 12, 6, 16, 28, 37, 135, DateTimeKind.Local), "List Role", "ListRole", true }
                });

            migrationBuilder.InsertData(
                schema: "AccessControl",
                table: "Role",
                columns: new[] { "Id", "CreationDate", "Description", "Name", "Status" },
                values: new object[] { new Guid("56462d64-83e7-4834-a30b-a87eca134c94"), new DateTime(2018, 12, 6, 16, 28, 37, 133, DateTimeKind.Local), "Role of system administrator", "AdministratorRole", true });

            migrationBuilder.InsertData(
                schema: "AccessControl",
                table: "User",
                columns: new[] { "Id", "CreationDate", "Email", "Name", "Password", "Status" },
                values: new object[] { new Guid("6364eb7f-dd2b-4ae0-b874-7b336aabee2f"), new DateTime(2018, 12, 6, 16, 28, 37, 115, DateTimeKind.Local), "admin@admin.com", "AdministratorUser", "Change123", true });

            migrationBuilder.InsertData(
                schema: "AccessControl",
                table: "RolePermission",
                columns: new[] { "IdRole", "IdPermission" },
                values: new object[,]
                {
                    { new Guid("56462d64-83e7-4834-a30b-a87eca134c94"), new Guid("048e322a-dc47-4094-b4d0-acb594e0abb6") },
                    { new Guid("56462d64-83e7-4834-a30b-a87eca134c94"), new Guid("51e3b2ef-7ab1-4abf-9c48-868d7a6be851") },
                    { new Guid("56462d64-83e7-4834-a30b-a87eca134c94"), new Guid("717aa7a4-b58d-440a-a8f9-b20ba17bbf5d") },
                    { new Guid("56462d64-83e7-4834-a30b-a87eca134c94"), new Guid("fb7a64d7-99db-4966-b16e-f2a1f1f9d8aa") },
                    { new Guid("56462d64-83e7-4834-a30b-a87eca134c94"), new Guid("3e409dc4-2b30-49c2-ac0d-49ff0a7eca75") },
                    { new Guid("56462d64-83e7-4834-a30b-a87eca134c94"), new Guid("57b5f483-65ca-4e40-b4ee-f40b70eea058") },
                    { new Guid("56462d64-83e7-4834-a30b-a87eca134c94"), new Guid("96336698-2a7e-48bc-ba37-259f0c213872") },
                    { new Guid("56462d64-83e7-4834-a30b-a87eca134c94"), new Guid("6a817d7d-c494-4f5c-afaa-0799f7d1621c") }
                });

            migrationBuilder.InsertData(
                schema: "AccessControl",
                table: "UserRole",
                columns: new[] { "IdUser", "IdRole" },
                values: new object[] { new Guid("6364eb7f-dd2b-4ae0-b874-7b336aabee2f"), new Guid("56462d64-83e7-4834-a30b-a87eca134c94") });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_IdPermission",
                schema: "AccessControl",
                table: "RolePermission",
                column: "IdPermission");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_IdRole",
                schema: "AccessControl",
                table: "UserRole",
                column: "IdRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermission",
                schema: "AccessControl");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "AccessControl");

            migrationBuilder.DropTable(
                name: "Permission",
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
