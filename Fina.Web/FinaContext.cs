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

        public DbSet<User> tbl_users { get; set; }
        public DbSet<Saving> tbl_savings { get; set; }
        public DbSet<Security> tbl_security { get; set; }
        public DbSet<Expense> tbl_expenses { get; set; }
        public DbSet<Income> tbl_incomes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(c => c.Country)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .Property(c => c.Currency)
                .HasConversion<string>();
            


            // single expense

            modelBuilder.Entity<Expense>()
                .Property(c => c.Type)
                .HasConversion<string>();


            // security

            modelBuilder.Entity<Security>()
                .Property(c => c.Type)
                .HasConversion<string>();


            // savings

            modelBuilder.Entity<Saving>()
                .Property(c => c.Type)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);

        }

    }
}
