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
    [Migration("20190527110117_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fina.Lib.Database.Expense", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber");

                    b.Property<decimal>("Cost");

                    b.Property<string>("Creditor");

                    b.Property<bool>("Life");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<long?>("User")
                        .IsRequired();

                    b.Property<bool>("Variable");

                    b.HasKey("Id");

                    b.HasIndex("User");

                    b.ToTable("tbl_expenses");
                });

            modelBuilder.Entity("Fina.Lib.Database.Income", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<string>("Company");

                    b.Property<string>("Function");

                    b.Property<DateTime>("StartDate");

                    b.Property<long?>("User")
                        .IsRequired();

                    b.Property<bool>("Variable");

                    b.Property<int>("WorkHours");

                    b.HasKey("Id");

                    b.HasIndex("User");

                    b.ToTable("tbl_incomes");
                });

            modelBuilder.Entity("Fina.Lib.Database.Saving", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber");

                    b.Property<int>("Amount");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<bool>("Longterm");

                    b.Property<int>("Monthly");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<long?>("User")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("User");

                    b.ToTable("tbl_savings");
                });

            modelBuilder.Entity("Fina.Lib.Database.Security", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber");

                    b.Property<int>("Amount");

                    b.Property<string>("Description");

                    b.Property<int>("Monthly");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<long?>("User")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("User");

                    b.ToTable("tbl_security");
                });

            modelBuilder.Entity("Fina.Lib.Database.User", b =>
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

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int>("LifeFunds");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Negative");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("Positive");

                    b.Property<int>("Total");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("tbl_users");
                });

            modelBuilder.Entity("Fina.Lib.Database.Expense", b =>
                {
                    b.HasOne("Fina.Lib.Database.User", "FK")
                        .WithMany("Expenses")
                        .HasForeignKey("User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fina.Lib.Database.Income", b =>
                {
                    b.HasOne("Fina.Lib.Database.User", "FK")
                        .WithMany("Incomes")
                        .HasForeignKey("User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fina.Lib.Database.Saving", b =>
                {
                    b.HasOne("Fina.Lib.Database.User", "FK")
                        .WithMany("Savings")
                        .HasForeignKey("User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fina.Lib.Database.Security", b =>
                {
                    b.HasOne("Fina.Lib.Database.User", "FK")
                        .WithMany("Securities")
                        .HasForeignKey("User")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
