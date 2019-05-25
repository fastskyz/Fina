using Microsoft.EntityFrameworkCore.Migrations;

namespace Fina.Web.Migrations
{
    public partial class FKabstractClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FKId",
                table: "tbl_single_expenses",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "FKId",
                table: "tbl_security",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "FKId",
                table: "tbl_savings",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "FKId",
                table: "tbl_jobs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "FKId",
                table: "tbl_incomes",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "FKId",
                table: "tbl_expenses",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_single_expenses_FKId",
                table: "tbl_single_expenses",
                column: "FKId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_security_FKId",
                table: "tbl_security",
                column: "FKId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_savings_FKId",
                table: "tbl_savings",
                column: "FKId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_jobs_FKId",
                table: "tbl_jobs",
                column: "FKId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_incomes_FKId",
                table: "tbl_incomes",
                column: "FKId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_expenses_FKId",
                table: "tbl_expenses",
                column: "FKId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_expenses_tbl_users_FKId",
                table: "tbl_expenses",
                column: "FKId",
                principalTable: "tbl_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_incomes_tbl_users_FKId",
                table: "tbl_incomes",
                column: "FKId",
                principalTable: "tbl_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_jobs_tbl_incomes_FKId",
                table: "tbl_jobs",
                column: "FKId",
                principalTable: "tbl_incomes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_savings_tbl_users_FKId",
                table: "tbl_savings",
                column: "FKId",
                principalTable: "tbl_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_security_tbl_users_FKId",
                table: "tbl_security",
                column: "FKId",
                principalTable: "tbl_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_single_expenses_tbl_expenses_FKId",
                table: "tbl_single_expenses",
                column: "FKId",
                principalTable: "tbl_expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_expenses_tbl_users_FKId",
                table: "tbl_expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_incomes_tbl_users_FKId",
                table: "tbl_incomes");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_jobs_tbl_incomes_FKId",
                table: "tbl_jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_savings_tbl_users_FKId",
                table: "tbl_savings");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_security_tbl_users_FKId",
                table: "tbl_security");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_single_expenses_tbl_expenses_FKId",
                table: "tbl_single_expenses");

            migrationBuilder.DropIndex(
                name: "IX_tbl_single_expenses_FKId",
                table: "tbl_single_expenses");

            migrationBuilder.DropIndex(
                name: "IX_tbl_security_FKId",
                table: "tbl_security");

            migrationBuilder.DropIndex(
                name: "IX_tbl_savings_FKId",
                table: "tbl_savings");

            migrationBuilder.DropIndex(
                name: "IX_tbl_jobs_FKId",
                table: "tbl_jobs");

            migrationBuilder.DropIndex(
                name: "IX_tbl_incomes_FKId",
                table: "tbl_incomes");

            migrationBuilder.DropIndex(
                name: "IX_tbl_expenses_FKId",
                table: "tbl_expenses");

            migrationBuilder.DropColumn(
                name: "FKId",
                table: "tbl_single_expenses");

            migrationBuilder.DropColumn(
                name: "FKId",
                table: "tbl_security");

            migrationBuilder.DropColumn(
                name: "FKId",
                table: "tbl_savings");

            migrationBuilder.DropColumn(
                name: "FKId",
                table: "tbl_jobs");

            migrationBuilder.DropColumn(
                name: "FKId",
                table: "tbl_incomes");

            migrationBuilder.DropColumn(
                name: "FKId",
                table: "tbl_expenses");
        }
    }
}
