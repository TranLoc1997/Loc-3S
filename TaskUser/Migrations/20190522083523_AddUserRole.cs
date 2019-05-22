using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskUser.Migrations
{
    public partial class AddUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "User",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PassWord", "Role" },
                values: new object[] { "20:ADNL8SgakrmgTqLoZIu0zy10UZawnIZCAP4=", "Admin" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PassWord", "Role" },
                values: new object[] { "20:MlweUK/3YOr7rDuxeNYDDfuRLxe1Y97zL08=", "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "User");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "PassWord",
                value: "20:8SDUBHnCfeIksaX7XVC9A52wJJWEMA5gTl8=");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "PassWord",
                value: "20:t83KJst+qnxJnCrvWF+RQ1d2Z416AgbmTSs=");
        }
    }
}
