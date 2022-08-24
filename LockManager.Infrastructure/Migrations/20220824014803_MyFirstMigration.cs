using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockManager.Infrastructure.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Door",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Open = table.Column<bool>(type: "bit", nullable: false),
                    MinimumRoleAuthorized = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Door", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAuth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TokenExpires = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAuth", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Username",
                table: "User",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_Username1",
                table: "UserAuth",
                column: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Door");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserAuth");
        }
    }
}
