using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockManager.Infrastructure.Migrations
{
    public partial class userauthunique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Username1",
                table: "UserAuth");

            migrationBuilder.CreateIndex(
                name: "IX_Username1",
                table: "UserAuth",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Username1",
                table: "UserAuth");

            migrationBuilder.CreateIndex(
                name: "IX_Username1",
                table: "UserAuth",
                column: "Username");
        }
    }
}
