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

        public DbSet<users> users { get; set; }
        public DbSet<savings> savings { get; set; }
        public DbSet<jobs> jobs { get; set; }
        public DbSet<security> security { get; set; }
        public DbSet<expenses> expenses { get; set; }
        public DbSet<single_expense> single_expenses { get; set; }
        public DbSet<incomes> incomes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<users>()
                .Property(c => c.Country)
                .HasConversion<string>();

            modelBuilder.Entity<users>()
                .Property(c => c.Currency)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);

        }

    }
}
