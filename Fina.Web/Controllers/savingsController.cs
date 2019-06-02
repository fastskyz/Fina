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
    public class SavingsController : Controller
    {
        private readonly FinaContext _context;




        public SavingsController(FinaContext context)
        {
            _context = context;
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




        // GET: Savings
        public async Task<IActionResult> Index()
        {
            if (IsLoggedIn())
            {
                string data = HttpContext.Session.GetString("User");
                UserSessionModel userSession = JsonConvert.DeserializeObject<UserSessionModel>(data);

                var user = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == userSession.Id);

                decimal TotalSaved = 0;
                decimal MonthlySaved = 0;

                SavingsOverviewVm savingsOverviewVm = new SavingsOverviewVm();
                savingsOverviewVm.Savings = _context.Entry(user).Collection(u => u.Savings).Query().AsEnumerable();
                savingsOverviewVm.nSavings = savingsOverviewVm.Savings.Count();

                foreach ( var sav in savingsOverviewVm.Savings )
                {
                    TotalSaved += sav.Amount;
                }

                savingsOverviewVm.Total = TotalSaved;

                foreach (var sav in savingsOverviewVm.Savings)
                {
                    TotalSaved += sav.Monthly;
                }

                savingsOverviewVm.Monthly = MonthlySaved;

                return View(savingsOverviewVm);
            }

            return RedirectToAction("Login", "User");
        }











        // GET: Savings/Add
        public IActionResult Add()
        {
            if (IsLoggedIn())
            {
                SavingsAddVm savingsAddVm = new SavingsAddVm();

                return View(savingsAddVm);
            }

            return RedirectToAction("Login", "User");
        }

        // POST: Savings/Add

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Name,Longterm,Monthly,Type,Description,AccountNumber,StartDate")] SavingsAddVm savingVm)
        {
            if (ModelState.IsValid)
            {
                string data = HttpContext.Session.GetString("User");
                UserSessionModel userSession = JsonConvert.DeserializeObject<UserSessionModel>(data);

                var fk = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == userSession.Id);

                Saving newSavings = new Saving {
                    FK = fk,

                    Name = savingVm.Name,
                    Longterm = savingVm.Longterm,
                    Monthly = savingVm.Monthly,
                    Type = savingVm.Type,

                    Description = savingVm.Description,
                    AccountNumber = savingVm.AccountNumber,
                    StartDate = DateTime.Parse(savingVm.StartDate)
                };

                _context.Add(newSavings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(savingVm);
        }














        // GET: Savings/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (IsLoggedIn())
            {
                Saving currentSavings = await _context.tbl_savings.Where(e => e.Id == id).FirstAsync();
                SavingsAddVm savingAddVm = new SavingsAddVm
                {
                    Name = currentSavings.Name,
                    Longterm = currentSavings.Longterm,
                    Monthly = currentSavings.Monthly,
                    Type = currentSavings.Type,

                    Description = currentSavings.Description,
                    AccountNumber = currentSavings.AccountNumber,
                    StartDate = currentSavings.StartDate.ToString("dd/MM/yyyy")
                };

                return View(savingAddVm);
            }

            return RedirectToAction("Login", "User");
            
        }

        // POST: Savings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Longterm,Monthly,Type,Description,AccountNumber,StartDate")] SavingsAddVm savingVm)
        {
            if (ModelState.IsValid)
            {
                Saving updatedSaving = await _context.tbl_savings.Where(e => e.Id == id).FirstAsync();

                // Copy over properties
                updatedSaving.Name = savingVm.Name;
                updatedSaving.Monthly = savingVm.Monthly;
                updatedSaving.Longterm = savingVm.Longterm;
                updatedSaving.Type = savingVm.Type;

                updatedSaving.Description = savingVm.Description;
                updatedSaving.AccountNumber = savingVm.AccountNumber;
                updatedSaving.StartDate = DateTime.Parse(savingVm.StartDate);

                try
                {
                    _context.Update(updatedSaving);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SavingsExists(updatedSaving.Id))
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

            return View(savingVm);
            
        }

















        // GET: Savings/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (IsLoggedIn())
            {
                if (id == null)
                {
                    return NotFound();
                }

                Saving saving = await _context.tbl_savings.Where(e => e.Id == id).FirstAsync();
                SavingsDeleteVm savingDelete = new SavingsDeleteVm
                {
                    Id = saving.Id,

                    Name = saving.Name,
                    Amount = saving.Amount,
                    Monthly = saving.Monthly,
                    Longterm = saving.Longterm,
                    Type = saving.Type,
                };

                return View(savingDelete);
            }

            return RedirectToAction("Login", "User");

            
        }

        // POST: Savings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var saving = await _context.tbl_savings.FindAsync(id);
            _context.tbl_savings.Remove(saving);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SavingsExists(long id)
        {
            return _context.tbl_savings.Any(e => e.Id == id);
        }
    }
}
