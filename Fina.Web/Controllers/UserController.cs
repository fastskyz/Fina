﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security;
using Fina.Lib.Database;
using Fina.Web.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Fina.Web.Controllers
{
    public class UserController : Controller
    {

        private readonly FinaContext _context;

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
       

        







        // GET: User
        public async Task<IActionResult> Index()
        {
            long id = 1;

            var user = await _context.tbl_users
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            user = await UpdateUserData(user);
            
            DetailsVm detailsVm = new DetailsVm();

            detailsVm.user = user;

            return View(detailsVm);
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
            string STATEKEY = "User Data";

            User user = await _context.tbl_users.Where(u => u.Email == details.Email).FirstAsync();

            string userData = JsonConvert.SerializeObject(user);

            HttpContext.Session.SetString(STATEKEY ,userData);

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

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
















        // GET: User/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var User = await _context.tbl_users.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            return View(User);
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
