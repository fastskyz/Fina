using Microsoft.EntityFrameworkCore;
using Fina.Lib.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fina.Lib.Database
{
    public class FinaContext : DbContext
    {
        public FinaContext(DbContextOptions<FinaContext> options) : base(options)
        {
            
        }

        public DbSet<users> tbl_users { get; set; }
        public DbSet<savings> tbl_savings { get; set; }
        public DbSet<jobs> tbl_jobs { get; set; }
        public DbSet<security> tbl_security { get; set; }
        public DbSet<expenses> tbl_expenses { get; set; }
        public DbSet<single_expense> tbl_single_expenses { get; set; }
        public DbSet<incomes> tbl_incomes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // users

            modelBuilder.Entity<users>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<users>()
                .Property(c => c.Country)
                .HasConversion<string>();

            modelBuilder.Entity<users>()
                .Property(c => c.Currency)
                .HasConversion<string>();
            


            // single expense

            modelBuilder.Entity<single_expense>()
                .Property(c => c.Type)
                .HasConversion<string>();


            // security

            modelBuilder.Entity<security>()
                .Property(c => c.Type)
                .HasConversion<string>();


            // savings

            modelBuilder.Entity<savings>()
                .Property(c => c.Type)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);

        }

    }
}
