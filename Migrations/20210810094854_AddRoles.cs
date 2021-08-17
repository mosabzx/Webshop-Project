using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b286bfa0-dfee-415f-866b-be16c32c8ff1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "774298ae-bd31-4fca-97f0-53e0726a4e07", "774298ae-bd31-4fca-97f0-53e0726a4e07", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "774298ae-bd31-4fca-97f0-53e0726a4e07");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b286bfa0-dfee-415f-866b-be16c32c8ff1", "b286bfa0-dfee-415f-866b-be16c32c8ff1", "Admin", "ADMIN" });
        }
    }
}
