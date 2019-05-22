using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskUser.Migrations
{
    public partial class dbcontex1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "PassWord",
                value: "20:jHgDfRC8G1VOYwvmRrxTk84iMXQDHyAjxW4=");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "PassWord",
                value: "20:ED/0RdnyC0WrkdhMxdJGKfo82SC9jYcZe5g=");
        }
    }
}
