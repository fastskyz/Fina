using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Security;
using Fina.Lib.Database;
using Fina.Web.Models;

namespace Fina.Web.Controllers
{
    public class UserController : Controller
    {

        private readonly FinaContext _context;

        public UserController(FinaContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.tbl_users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            DetailsVm detailsVm = new DetailsVm();

            detailsVm.user = user;
            detailsVm.negative = user.Negative;
            detailsVm.positive = user.Positive;

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
            SHA256 newSHA256 = SHA256.Create(details.Password);

            users user = _context.tbl_users.Where(u => u.Email == details.Email).First();
            
            if ( newSHA256.ToString() == user.Password )
            {
                // do something
            }

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
        public async Task<IActionResult> SignUp([Bind("Id,Name,FirstName,Email,Country,Password,Age,Currency")] users User)
        {
            if (ModelState.IsValid)
            {
                expenses Negative = new expenses();
                Negative.FK = User;

                incomes Positive = new incomes();
                Positive.FK = User;

                _context.Add(User);
                _context.Add(Negative);
                _context.Add(Positive);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(User);
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
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,FirstName,Email,Country,Password,Age,Currency")] users User)
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
