using Microsoft.EntityFrameworkCore.Migrations;

namespace Fina.Web.Migrations
{
    public partial class fk5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_expenses_tbl_users_FKId",
                table: "tbl_expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_incomes_tbl_users_FKId",
                table: "tbl_incomes");

            migrationBuilder.DropIndex(
                name: "IX_tbl_incomes_FKId",
                table: "tbl_incomes");

            migrationBuilder.DropIndex(
                name: "IX_tbl_expenses_FKId",
                table: "tbl_expenses");

            migrationBuilder.DropColumn(
                name: "FKId",
                table: "tbl_incomes");

            migrationBuilder.DropColumn(
                name: "FKId",
                table: "tbl_expenses");

            migrationBuilder.AddColumn<long>(
                name: "ExpensesId",
                table: "tbl_users",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IncomeId",
                table: "tbl_users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_users_ExpensesId",
                table: "tbl_users",
                column: "ExpensesId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_users_IncomeId",
                table: "tbl_users",
                column: "IncomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_users_tbl_expenses_ExpensesId",
                table: "tbl_users",
                column: "ExpensesId",
                principalTable: "tbl_expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_users_tbl_incomes_IncomeId",
                table: "tbl_users",
                column: "IncomeId",
                principalTable: "tbl_incomes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_users_tbl_expenses_ExpensesId",
                table: "tbl_users");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_users_tbl_incomes_IncomeId",
                table: "tbl_users");

            migrationBuilder.DropIndex(
                name: "IX_tbl_users_ExpensesId",
                table: "tbl_users");

            migrationBuilder.DropIndex(
                name: "IX_tbl_users_IncomeId",
                table: "tbl_users");

            migrationBuilder.DropColumn(
                name: "ExpensesId",
                table: "tbl_users");

            migrationBuilder.DropColumn(
                name: "IncomeId",
                table: "tbl_users");

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
        }
    }
}
