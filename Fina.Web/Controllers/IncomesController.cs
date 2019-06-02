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
    public class IncomesController : Controller
    {
        private readonly FinaContext _context;




        public IncomesController(FinaContext context)
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




        // GET: Incomes
        public async Task<IActionResult> Index()
        {
            if (IsLoggedIn())
            {
                string data = HttpContext.Session.GetString("User");
                UserSessionModel userSession = JsonConvert.DeserializeObject<UserSessionModel>(data);

                var user = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == userSession.Id);


                IncomesOverviewVm incomesOverviewVm = new IncomesOverviewVm();
                incomesOverviewVm.Incomes = _context.Entry(user).Collection(u => u.Incomes).Query().AsEnumerable();
                incomesOverviewVm.Total = incomesOverviewVm.Incomes.Count();
                incomesOverviewVm.WorkHours = incomesOverviewVm.WorkHours;
                incomesOverviewVm.Variable = incomesOverviewVm.Incomes.Where(e => e.Variable).Count();

                return View(incomesOverviewVm);
            }

            return RedirectToAction("Login", "User");
        }

        // GET: Incomes/Add
        public IActionResult Add()
        {
            if (IsLoggedIn())
            {
                IncomesAddVm incomesAddVm = new IncomesAddVm();

                return View(incomesAddVm);
            }

            return RedirectToAction("Login", "User");
        }

        // POST: Incomes/Add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // ModelState.IsValid

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Name,Amount,Variable,Amount,WorkHours,Function,Company,StartDate")] IncomesAddVm incomeVm)
        {
            if (ModelState.IsValid)
            {
                string data = HttpContext.Session.GetString("User");
                UserSessionModel userSession = JsonConvert.DeserializeObject<UserSessionModel>(data);

                var fk = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == userSession.Id);

                Income newIncomes = new Income {
                    FK = fk,

                    Name = incomeVm.Name,
                    Amount = incomeVm.Amount,
                    WorkHours = incomeVm.WorkHours,
                    Variable = incomeVm.Variable,

                    Function = incomeVm.Function,
                    Company = incomeVm.Company,
                    StartDate = incomeVm.StartDate
                };

                _context.Add(newIncomes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(incomeVm);
        }



        // GET: Incomes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (IsLoggedIn())
            {
                Income currentIncomes = await _context.tbl_incomes.Where(e => e.Id == id).FirstAsync();
                IncomesAddVm incomeAddVm = new IncomesAddVm
                {
                    Name = currentIncomes.Name,
                    Amount = currentIncomes.Amount,
                    WorkHours = currentIncomes.WorkHours,
                    Variable = currentIncomes.Variable,

                    Function = currentIncomes.Function,
                    Company = currentIncomes.Company,
                    StartDate = currentIncomes.StartDate
                };

                return View(incomeAddVm);
            }

            return RedirectToAction("Login", "User");
            
        }

        // POST: Incomes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Amount,Variable,Amount,WorkHours,Function,Company,StartDate")] IncomesAddVm incomeVm)
        {
            if (ModelState.IsValid)
            {
                Income updatedIncome = await _context.tbl_incomes.Where(e => e.Id == id).FirstAsync();

                // Copy over properties
                updatedIncome.Name = incomeVm.Name;
                updatedIncome.Variable = incomeVm.Variable;
                updatedIncome.Amount = incomeVm.Amount;
                updatedIncome.WorkHours = incomeVm.WorkHours;

                updatedIncome.Function = incomeVm.Function;
                updatedIncome.Company = incomeVm.Company;
                updatedIncome.StartDate = incomeVm.StartDate;

                try
                {
                    _context.Update(updatedIncome);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomesExists(updatedIncome.Id))
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

            return View(incomeVm);
            
        }

        // GET: Incomes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (IsLoggedIn())
            {
                if (id == null)
                {
                    return NotFound();
                }

                Income income = await _context.tbl_incomes.Where(e => e.Id == id).FirstAsync();
                IncomesDeleteVm incomeDelete = new IncomesDeleteVm
                {
                    Id = income.Id,

                    Name = income.Name,
                    Amount = income.Amount,
                    WorkHours = income.WorkHours,
                    Variable = income.Variable,
                };

                return View(incomeDelete);
            }

            return RedirectToAction("Login", "User");

            
        }

        // POST: Incomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var income = await _context.tbl_incomes.FindAsync(id);
            _context.tbl_incomes.Remove(income);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncomesExists(long id)
        {
            return _context.tbl_incomes.Any(e => e.Id == id);
        }
    }
}
