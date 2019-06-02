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


        protected override async void OnModelCreating(ModelBuilder modelBuilder)
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

            // Data Seeding //

            // users
            modelBuilder.Entity<User>().HasData(new User { Id = 1, Name = "De Langhe", FirstName = "Seppe", Email = "seppedelanghe17@gmail.com", Country = User.Countries.Belgium, Password = "delanghe", Age = 18, Currency = User.Currencies.Euro, Total = 0, LifeFunds = 0, Positive = 0, Negative = 0 },
                                                new User { Id = 2, Name = "Hunter", FirstName = "Troy", Email = "troy.hunter41@example.com", Country = User.Countries.USA, Password = "milano", Age = 37, Currency = User.Currencies.Dollar, Total = 0, LifeFunds = 0, Positive = 0, Negative = 0 },
                                                new User { Id = 3, Name = "Duncan", FirstName = "Kristina", Email = "kristinaduncan@example.com", Country = User.Countries.Engeland, Password = "tiao", Age = 32, Currency = User.Currencies.Pounds, Total = 0, LifeFunds = 0, Positive = 0, Negative = 0 }
                                                );

            // Expenses
            modelBuilder.Entity<Expense>().HasData( new Expense { Id = 1, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 1) , Name = "Rent", Life = true, Type = Expense.ExpenseType.Rent, Variable = false, Cost = 780, AccountNumber = "BE55 1234 1234 1234", Creditor = "Immo Thuis"},
                                                    new Expense { Id = 2, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 1), Name = "Shopping", Life = true, Type = Expense.ExpenseType.Consumables, Variable = true, Cost = 320 },
                                                    new Expense { Id = 3, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 1), Name = "Gasoline", Life = true, Type = Expense.ExpenseType.Car, Variable = true, Cost = 73 },

                                                    new Expense { Id = 4, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 2), Name = "House Rent", Life = true, Type = Expense.ExpenseType.Rent, Variable = false, Cost = 1040, AccountNumber = "US33 1234 1234 1234", Creditor = "Micheal" },
                                                    new Expense { Id = 5, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 2), Name = "Dog Food", Life = true, Type = Expense.ExpenseType.Consumables, Variable = true, Cost = 23, Creditor = "Walmart" },
                                                    new Expense { Id = 6, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 2), Name = "Netflix", Life = false, Type = Expense.ExpenseType.Subscription, Variable = false, Cost = 11.99M, AccountNumber = "US22 1234 1234 1234", Creditor = "Netflix Inc." },

                                                    new Expense { Id = 7, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 3), Name = "Appertment Loan", Life = true, Type = Expense.ExpenseType.Loan, Variable = false, Cost = 840, AccountNumber = "UK33 1234 1234 1234", Creditor = "Santander UK" },
                                                    new Expense { Id = 8, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 3), Name = "Cleaning", Life = true, Type = Expense.ExpenseType.Service, Variable = false, Cost = 49.99M, AccountNumber = "UK30 1234 1234 1234", Creditor = "CleanTeam" },
                                                    new Expense { Id = 9, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 3), Name = "Water", Life = true, Type = Expense.ExpenseType.Resources, Variable = true, Cost = 119.34M, AccountNumber = "UK03 1234 1234 1234", Creditor = "Columbu Water And Gas" }

                                                  );



            // Incomes

            modelBuilder.Entity<Income>().HasData(new Income { Id = 1, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 1), Amount = 1650, Company = "Facebook", Function = "Freelance Devoloper", Name = "Freelance", StartDate = DateTime.Now.AddMonths(-3).AddDays(-14), Variable = true, WorkHours = 38 },
                                                  new Income { Id = 2, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 2), Amount = 2190, Company = "Google", Function = "Lead UI Devoloper", Name = "Main Job", StartDate = DateTime.Now.AddMonths(-34).AddDays(-24), Variable = false, WorkHours = 43 },
                                                  new Income { Id = 3, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 3), Amount = 1450, Company = "Steve's Tables", Function = "Accountant", Name = "Main job", StartDate = DateTime.Now.AddMonths(-43).AddDays(45), Variable = false, WorkHours = 33 },
                                                  new Income { Id = 4, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 3), Amount = 330, Company = "McDonalds", Function = "Accountant", Name = "Weekend Job", StartDate = DateTime.Now.AddMonths(-16), Variable = true, WorkHours = 11 }
                                                  );


            // Savings
            modelBuilder.Entity<Saving>().HasData(new Saving { Id = 1, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 1), Amount = 430, StartDate = DateTime.Now.AddMonths(-43), Longterm = true, Type = Saving.savingsType.Travel, Description = "World Trip in 2023" , Name = "World Trip", AccountNumber = "BE62 1122 3344 5566", Monthly = 10},
                                                  new Saving { Id = 2, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 2), Amount = 43500, StartDate = DateTime.Now.AddYears(-23).AddMonths(-3), Longterm = true, Type = Saving.savingsType.Retirement, Description = "Retirement savings for a good life!", Name = "Retirement", AccountNumber = "US60 1122 3344 5566", Monthly = 125 },
                                                  new Saving { Id = 3, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 2), Amount = 30, StartDate = DateTime.Now.AddMonths(-3), Longterm = false, Type = Saving.savingsType.Hobby, Description = "Saving up for FIFA 20 in September", Name = "FIFA 20", AccountNumber = "US60 2122 3344 5566", Monthly = 10 },
                                                  new Saving { Id = 4, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 3), Amount = 8000, StartDate = DateTime.Now.AddMonths(-31), Longterm = true, Type = Saving.savingsType.Childeren, Description = "Saving up for the childeren", Name = "Kiddies", AccountNumber = "UK21 1122 4432 5566", Monthly = 50 }
                                                  );



            // Security

            modelBuilder.Entity<Security>().HasData(new Security { Id = 1, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 2), Amount = 200, StartDate = DateTime.Now.AddMonths(-20), Monthly = 10, Type = Security.secure_type.Runway, Description = "If the money flow ends" },
                                                    new Security { Id = 2, FK = await tbl_users.FirstOrDefaultAsync(u => u.Id == 3), Amount = 1000, StartDate = DateTime.Now.AddYears(-2).AddMonths(-3), Monthly = 20, Type = Security.secure_type.Guard, Description = "If something breaks down" }
                                                  );


            base.OnModelCreating(modelBuilder);

        }

    }
}
