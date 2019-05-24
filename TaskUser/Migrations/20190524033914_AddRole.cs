using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskUser.Migrations
{
    public partial class AddRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PassWord", "Role" },
                values: new object[] { "20:IzUTWWVHY7Kr8uMis2MpmoAMVfJIpoROHkE=", 1 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PassWord", "Role" },
                values: new object[] { "20:6wjMBj/PeqI9u2dunQEVhNMpVQzuajckNl0=", 2 });
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
                value: "20:donJpATzjZv/W+uO8T36GkeozEoxgnF/gCM=");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "PassWord",
                value: "20:w7JhNzg/0RhSto6OaUedyU4kQIN0pmZGOM8=");
        }
    }
}
