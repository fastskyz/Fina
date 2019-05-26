﻿// <auto-generated />
using System;
using Fina.Lib.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fina.Web.Migrations
{
    [DbContext(typeof(FinaContext))]
    [Migration("20190526191617_fk5")]
    partial class fk5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fina.Lib.Database.expenses", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LifeFunds");

                    b.Property<int>("Total");

                    b.HasKey("Id");

                    b.ToTable("tbl_expenses");
                });

            modelBuilder.Entity("Fina.Lib.Database.incomes", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Total");

                    b.Property<int>("TotalWorkHours");

                    b.HasKey("Id");

                    b.ToTable("tbl_incomes");
                });

            modelBuilder.Entity("Fina.Lib.Database.jobs", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Company");

                    b.Property<long>("FKId");

                    b.Property<string>("Function");

                    b.Property<int>("Income");

                    b.Property<DateTime>("StartDate");

                    b.Property<bool>("Variable");

                    b.Property<int>("WorkHours");

                    b.HasKey("Id");

                    b.HasIndex("FKId");

                    b.ToTable("tbl_jobs");
                });

            modelBuilder.Entity("Fina.Lib.Database.savings", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber");

                    b.Property<int>("Amount");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<long>("FKId");

                    b.Property<bool>("Longterm");

                    b.Property<int>("Monthly");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("FKId");

                    b.ToTable("tbl_savings");
                });

            modelBuilder.Entity("Fina.Lib.Database.security", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber");

                    b.Property<int>("Amount");

                    b.Property<string>("Description");

                    b.Property<long>("FKId");

                    b.Property<int>("Monthly");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("FKId");

                    b.ToTable("tbl_security");
                });

            modelBuilder.Entity("Fina.Lib.Database.single_expense", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber");

                    b.Property<decimal>("Cost");

                    b.Property<string>("Creditor");

                    b.Property<long>("FKId");

                    b.Property<bool>("Life");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<bool>("Variable");

                    b.HasKey("Id");

                    b.HasIndex("FKId");

                    b.ToTable("tbl_single_expenses");
                });

            modelBuilder.Entity("Fina.Lib.Database.users", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Age");

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<string>("Currency")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<long?>("ExpensesId");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<long?>("IncomeId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ExpensesId");

                    b.HasIndex("IncomeId");

                    b.ToTable("tbl_users");
                });

            modelBuilder.Entity("Fina.Lib.Database.jobs", b =>
                {
                    b.HasOne("Fina.Lib.Database.incomes", "FK")
                        .WithMany("Jobs")
                        .HasForeignKey("FKId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fina.Lib.Database.savings", b =>
                {
                    b.HasOne("Fina.Lib.Database.users", "FK")
                        .WithMany("Savings")
                        .HasForeignKey("FKId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fina.Lib.Database.security", b =>
                {
                    b.HasOne("Fina.Lib.Database.users", "FK")
                        .WithMany("Securities")
                        .HasForeignKey("FKId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fina.Lib.Database.single_expense", b =>
                {
                    b.HasOne("Fina.Lib.Database.expenses", "FK")
                        .WithMany("Singles")
                        .HasForeignKey("FKId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fina.Lib.Database.users", b =>
                {
                    b.HasOne("Fina.Lib.Database.expenses", "Expenses")
                        .WithMany()
                        .HasForeignKey("ExpensesId");

                    b.HasOne("Fina.Lib.Database.incomes", "Income")
                        .WithMany()
                        .HasForeignKey("IncomeId");
                });
#pragma warning restore 612, 618
        }
    }
}
