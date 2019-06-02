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
                if ( SHA256.Create(user.Password) == SHA256.Create(details.Password) )
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
















        // GET: User/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (IsLoggedIn())
            {
                if (id == null)
                {
                    return NotFound();
                }


                string data = HttpContext.Session.GetString("User");
                UserSessionModel userSession = JsonConvert.DeserializeObject<UserSessionModel>(data);

                var user = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == userSession.Id);

                return View(User);

            }
            return RedirectToAction(nameof(Login));

    }

    // POST: User/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,FirstName,Email,Country,Password,Age,Currency")] User User)
        {
            if (id != User.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(User);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(User.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(User);
        }


















        private bool UserExists(long id)
        {
            return _context.tbl_users.Any(e => e.Id == id);
        }
    }
}
