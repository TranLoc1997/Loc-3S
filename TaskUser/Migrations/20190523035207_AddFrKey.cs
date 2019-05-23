using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskUser.Migrations
{
    public partial class AddFrKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "PassWord",
                value: "20:B0kSQJNzlKXOY7zBC9P+65aOVJUjWXpOd/Q=");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "PassWord",
                value: "20:HezTL5Xu2PLFB7jCv2iMXbPOuAC5aIKMmrU=");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "PassWord",
                value: "20:ADNL8SgakrmgTqLoZIu0zy10UZawnIZCAP4=");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "PassWord",
                value: "20:MlweUK/3YOr7rDuxeNYDDfuRLxe1Y97zL08=");
        }
    }
}
