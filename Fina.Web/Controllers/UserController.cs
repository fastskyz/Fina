using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

using Newtonsoft.Json;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Fina.Lib.Database;
using Fina.Web.Models;
using Fina.Web.Models.UserModels;




namespace Fina.Web.Controllers
{
    public class UserController : Controller
    {

        private readonly FinaContext _context;

        public string STATEKEY = "UserData";

        public object HttpSessionState { get; private set; }

        public UserController(FinaContext context)
        {
            _context = context;
        }

        public async Task<User> UpdateUserData(User user)
        {
            user.Total = 0;
            user.Negative = 0;
            user.Positive = 0;

            // update expenses
            var expenses = _context.Entry(user).Collection(u => u.Expenses).Query().AsEnumerable();
            if (expenses != null)
            {
                foreach (var exp in expenses)
                {
                    user.Negative += exp.Cost;
                }
            }

            // update incomes

            
            var incomes = _context.Entry(user).Collection(u => u.Incomes).Query().AsEnumerable();

            if (incomes != null)
            {
                foreach (var inc in incomes)
                {
                    user.Positive += inc.Amount;
                }
            }
            
            user.Total = user.Positive - user.Negative;

            // save changes to db
            _context.Update(user);
            await _context.SaveChangesAsync();

            // return user to display details
            return user;
        }


        public bool IsLoggedIn()
        {
            if (HttpContext.Session.GetString("User") != null)
            {

                string data = HttpContext.Session.GetString("User");
                UserSessionModel userSession = JsonConvert.DeserializeObject<UserSessionModel>(data);

                if (userSession.Id != 0)
                {
                    return _context.tbl_users.Any(u => u.Id == userSession.Id);
                }
            }

            return false;
        }





















        // GET: User
        public async Task<IActionResult> Index()
        {
            if ( IsLoggedIn() )
            {
                string data = HttpContext.Session.GetString("User");
                UserSessionModel userSession = JsonConvert.DeserializeObject<UserSessionModel>(data);

                var user = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == userSession.Id);

                DetailsVm detailsVm = new DetailsVm { user = await UpdateUserData(user) } ;
                return View(detailsVm);
                
            }
            return RedirectToAction(nameof(Login));
        }















        // GET: Login
        public IActionResult Login()
        {
            var user = new LoginVm();

            return View(user);
        }
        

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] LoginVm details)
        {
            User user = await _context.tbl_users.Where(u => u.Email == details.Email).FirstOrDefaultAsync();

            if ( user != null )
            {
                if ( user.Password == SHA256.Create(details.Password).ToString() )
                {
                    UserSessionModel userSession = new UserSessionModel();

                    userSession.Id = user.Id;
                    userSession.FName = user.FirstName;
                    
                    string json = JsonConvert.SerializeObject(userSession);
                    

                    HttpContext.Session.SetString("User", json);


                    TempData["Message"] = $"Welcome, {userSession.FName}. You are now logged in.";
                    TempData["MessageType"] = "success";

                    return RedirectToAction(nameof(Index));
                }
            }


            TempData["Message"] = "Login Failed, please try again.";
            TempData["MessageType"] = "warning";

            return View();
        }
















        // GET: SignUp
        public IActionResult SignUp()
        {
            var user = new SignUpVm();

            return View(user);
        }

        // POST: Sign Up
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("Id,Name,FirstName,Email,Country,Password,Age,Currency")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = SHA256.Create(user.Password).ToString();

                _context.Add(user);

                TempData["Message"] = "Account created, you can now login.";
                TempData["MessageType"] = "success";

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }

            TempData["Message"] = "Register Failed, please try again.";
            TempData["MessageType"] = "alert";

            return View(user);
        }
















        private bool UserExists(long id)
        {
            return _context.tbl_users.Any(e => e.Id == id);
        }
















        ///
        /// Data seeding
        /// 






        // GET: Seed
        public async Task<IActionResult> Seed()
        {
            // Data Seeding //
            // users
            _context.AddRange(
                new User { Name = "De Langhe", FirstName = "Seppe", Email = "seppedelanghe17@gmail.com", Country =  Lib.Database.User.Countries.Belgium, Password = SHA256.Create("delanghe").ToString(), Age = 18, Currency = Lib.Database.User.Currencies.Euro, Total = 0, LifeFunds = 0, Positive = 0, Negative = 0 },
                new User { Name = "Hunter", FirstName = "Troy", Email = "troy.hunter41@example.com", Country = Lib.Database.User.Countries.USA, Password = SHA256.Create("milano").ToString(), Age = 37, Currency = Lib.Database.User.Currencies.Dollar, Total = 0, LifeFunds = 0, Positive = 0, Negative = 0 },
                new User { Name = "Duncan", FirstName = "Kristina", Email = "kristinaduncan@example.com", Country = Lib.Database.User.Countries.Engeland, Password = SHA256.Create("tiao").ToString(), Age = 32, Currency = Lib.Database.User.Currencies.Pounds, Total = 0, LifeFunds = 0, Positive = 0, Negative = 0 }
                );

            await _context.SaveChangesAsync();

            // Expenses
            _context.AddRange(
                new Expense { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 1), Name = "Rent", Life = true, Type = Expense.ExpenseType.Rent, Variable = false, Cost = 780, AccountNumber = "BE55 1234 1234 1234", Creditor = "Immo Thuis" },
                new Expense { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 1), Name = "Shopping", Life = true, Type = Expense.ExpenseType.Consumables, Variable = true, Cost = 320 },
                new Expense { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 1), Name = "Gasoline", Life = true, Type = Expense.ExpenseType.Car, Variable = true, Cost = 73 },

                new Expense { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 2), Name = "House Rent", Life = true, Type = Expense.ExpenseType.Rent, Variable = false, Cost = 1040, AccountNumber = "US33 1234 1234 1234", Creditor = "Micheal" },
                new Expense { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 2), Name = "Dog Food", Life = true, Type = Expense.ExpenseType.Consumables, Variable = true, Cost = 23, Creditor = "Walmart" },
                new Expense { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 2), Name = "Netflix", Life = false, Type = Expense.ExpenseType.Subscription, Variable = false, Cost = 11.99M, AccountNumber = "US22 1234 1234 1234", Creditor = "Netflix Inc." },

                new Expense { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 3), Name = "Appertment Loan", Life = true, Type = Expense.ExpenseType.Loan, Variable = false, Cost = 840, AccountNumber = "UK33 1234 1234 1234", Creditor = "Santander UK" },
                new Expense { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 3), Name = "Cleaning", Life = true, Type = Expense.ExpenseType.Service, Variable = false, Cost = 49.99M, AccountNumber = "UK30 1234 1234 1234", Creditor = "CleanTeam" },
                new Expense { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 3), Name = "Water", Life = true, Type = Expense.ExpenseType.Resources, Variable = true, Cost = 119.34M, AccountNumber = "UK03 1234 1234 1234", Creditor = "Columbu Water And Gas" }
                );



            // Incomes
            _context.AddRange(
                new Income { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 1), Amount = 1650, Company = "Facebook", Function = "Freelance Devoloper", Name = "Freelance", StartDate = DateTime.Now.AddMonths(-3).AddDays(-14), Variable = true, WorkHours = 38 },
                new Income { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 2), Amount = 2190, Company = "Google", Function = "Lead UI Devoloper", Name = "Main Job", StartDate = DateTime.Now.AddMonths(-34).AddDays(-24), Variable = false, WorkHours = 43 },
                new Income { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 3), Amount = 1450, Company = "Steve's Tables", Function = "Accountant", Name = "Main job", StartDate = DateTime.Now.AddMonths(-43).AddDays(45), Variable = false, WorkHours = 33 },
                new Income { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 3), Amount = 330, Company = "McDonalds", Function = "Accountant", Name = "Weekend Job", StartDate = DateTime.Now.AddMonths(-16), Variable = true, WorkHours = 11 }
                );


            // Savings
            _context.AddRange(
                new Saving { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 1), Amount = 430, StartDate = DateTime.Now.AddMonths(-43), Longterm = true, Type = Saving.savingsType.Travel, Description = "World Trip in 2023", Name = "World Trip", AccountNumber = "BE62 1122 3344 5566", Monthly = 10 },
                new Saving { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 2), Amount = 43500, StartDate = DateTime.Now.AddYears(-23).AddMonths(-3), Longterm = true, Type = Saving.savingsType.Retirement, Description = "Retirement savings for a good life!", Name = "Retirement", AccountNumber = "US60 1122 3344 5566", Monthly = 125 },
                new Saving { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 2), Amount = 30, StartDate = DateTime.Now.AddMonths(-3), Longterm = false, Type = Saving.savingsType.Hobby, Description = "Saving up for FIFA 20 in September", Name = "FIFA 20", AccountNumber = "US60 2122 3344 5566", Monthly = 10 },
                new Saving { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 3), Amount = 8000, StartDate = DateTime.Now.AddMonths(-31), Longterm = true, Type = Saving.savingsType.Childeren, Description = "Saving up for the childeren", Name = "Kiddies", AccountNumber = "UK21 1122 4432 5566", Monthly = 50 }
                );



            // Security
            _context.AddRange(
                new Security { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 2), Amount = 200, StartDate = DateTime.Now.AddMonths(-20), Monthly = 10, Type = Security.secure_type.Runway, Description = "If the money flow ends" },
                new Security { FK = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == 3), Amount = 1000, StartDate = DateTime.Now.AddYears(-2).AddMonths(-3), Monthly = 20, Type = Security.secure_type.Guard, Description = "If something breaks down" }
                );

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Login));
        }










    }
}
