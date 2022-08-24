using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockManager.Infrastructure.Migrations
{
    public partial class usernameunique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Username",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_Username",
                table: "User",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Username",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_Username",
                table: "User",
                column: "Username");
        }
    }
}
