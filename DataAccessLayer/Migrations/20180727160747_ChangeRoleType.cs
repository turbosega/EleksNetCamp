using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class ChangeRoleType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "ImageSrc",
                table: "Games",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "PasswordHash", "UserType" },
                values: new object[] { 1, "Fry", "AQAAAAEAACcQAAAAEAyTA6a5i6/Ns6VkkhDgh345S9gj+aYUPTmJRBO2TJzQdMOxJApGpWn9k6XL5+VtfA==", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "ImageSrc",
                table: "Games");

            migrationBuilder.AlterColumn<string>(
                name: "UserType",
                table: "Users",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
