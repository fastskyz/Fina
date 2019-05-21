using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fina.Web.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_expenses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Total = table.Column<int>(nullable: false),
                    LifeFunds = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_expenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_incomes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Total = table.Column<int>(nullable: false),
                    TotalWorkHours = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_incomes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_jobs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Income = table.Column<int>(nullable: false),
                    Variable = table.Column<bool>(nullable: false),
                    WorkHours = table.Column<int>(nullable: false),
                    Function = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_savings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    Longterm = table.Column<bool>(nullable: false),
                    Monthly = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    AccountNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_savings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_security",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Monthly = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_security", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_single_expenses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Life = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Variable = table.Column<bool>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    AccountNumber = table.Column<string>(nullable: true),
                    Creditor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_single_expenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Age = table.Column<byte>(nullable: false),
                    Currency = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_expenses");

            migrationBuilder.DropTable(
                name: "tbl_incomes");

            migrationBuilder.DropTable(
                name: "tbl_jobs");

            migrationBuilder.DropTable(
                name: "tbl_savings");

            migrationBuilder.DropTable(
                name: "tbl_security");

            migrationBuilder.DropTable(
                name: "tbl_single_expenses");

            migrationBuilder.DropTable(
                name: "tbl_users");
        }
    }
}
