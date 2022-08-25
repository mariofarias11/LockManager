using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LockManager.Infrastructure.Migrations
{
    public partial class adminseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(table: "User", 
                columns: new string[]{ "Username", "Role", "Active" }, 
                values: new object[] { "admin", 100, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
