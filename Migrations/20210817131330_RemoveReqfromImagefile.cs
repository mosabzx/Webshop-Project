using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop.Migrations
{
    public partial class RemoveReqfromImagefile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d48e8ed-1ef0-491a-b3b4-37790cccf87a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "85005cc4-167d-4a5a-8d63-642a3dce80f5", "85005cc4-167d-4a5a-8d63-642a3dce80f5", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85005cc4-167d-4a5a-8d63-642a3dce80f5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0d48e8ed-1ef0-491a-b3b4-37790cccf87a", "0d48e8ed-1ef0-491a-b3b4-37790cccf87a", "Admin", "ADMIN" });
        }
    }
}
