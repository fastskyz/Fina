using Microsoft.EntityFrameworkCore.Migrations;

namespace Fina.Web.Migrations
{
    public partial class usersFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "tbl_users",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_tbl_users_Email",
                table: "tbl_users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tbl_users_Email",
                table: "tbl_users");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "tbl_users",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
