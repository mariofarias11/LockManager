using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockManager.Infrastructure.Migrations
{
    public partial class doorhistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoorHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsSuccessfulEntry = table.Column<bool>(type: "bit", nullable: false),
                    EntryDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Door_Id",
                        column: x => x.DoorId,
                        principalTable: "Door",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Door",
                table: "DoorHistory",
                column: "DoorId");

            migrationBuilder.CreateIndex(
                name: "IX_User",
                table: "DoorHistory",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoorHistory");
        }
    }
}
