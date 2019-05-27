using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fina.Lib.Database;
using Fina.Web.Models;

namespace Fina.Web.Controllers
{
    public class IncomesController : Controller
    {
        private readonly FinaContext _context;

        public IncomesController(FinaContext context)
        {
            _context = context;
        }

        // GET: Incomes
        public async Task<IActionResult> Index()
        {
            long id = 1;
            User user = await _context.tbl_users.Where(u => u.Id == id).FirstAsync();
            IncomesOverviewVm incomesOverviewVm = new IncomesOverviewVm();

            incomesOverviewVm.Incomes = _context.Entry(user).Collection(u => u.Incomes).Query().AsEnumerable();
            incomesOverviewVm.Total = incomesOverviewVm.Incomes.Count();

            incomesOverviewVm.Life = incomesOverviewVm.Incomes.Where(e => e.Life).Count();
            incomesOverviewVm.Variable = incomesOverviewVm.Incomes.Where(e => e.Variable).Count();

            return View(incomesOverviewVm);
        }

        // GET: Incomes/Add
        public IActionResult Add()
        {
            IncomesAddVm expenseAddVm = new IncomesAddVm();

            return View(expenseAddVm);
        }

        // POST: Incomes/Add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // ModelState.IsValid

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Name,Life,Type,Variable,Cost,AccountNumber,Creditor,expenseTypes")] IncomesAddVm expenseVm)
        {
            if (ModelState.IsValid)
            {
                var fk = await _context.tbl_users.Where(u => u.Id == 1).FirstAsync();

                Income newIncomes = new Income {
                    FK = fk,
                    Name = expenseVm.Name,
                    AccountNumber = expenseVm.AccountNumber,
                    Life = expenseVm.Life,
                    Type = expenseVm.Type,
                    Variable = expenseVm.Variable,
                    Cost = expenseVm.Cost,
                    Creditor = expenseVm.Creditor
                };

                _context.Add(newIncomes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenseVm);
        }



        // GET: Incomes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            Income currentIncomes  = await _context.tbl_incomes.Where(e => e.Id == id).FirstAsync();
            IncomesAddVm expenseAddVm = new IncomesAddVm
            {
                Name = currentIncomes.Name,
                Life = currentIncomes.Life,
                Type = currentIncomes.Type,
                Variable = currentIncomes.Variable,
                Cost = currentIncomes.Cost,

                AccountNumber = currentIncomes.AccountNumber,
                Creditor = currentIncomes.Creditor
            };

            return View(expenseAddVm);
        }

        // POST: Incomes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Life,Type,Variable,Cost,AccountNumber,Creditor,expenseTypes")] IncomesAddVm expenseVm)
        {
            if (ModelState.IsValid)
            {
                Income updatedIncomes = await _context.tbl_incomes.Where(e => e.Id == id).FirstAsync();

                // Copy over properties
                updatedIncomes.Name = expenseVm.Name;
                updatedIncomes.Life = expenseVm.Life;
                updatedIncomes.Type = expenseVm.Type;
                updatedIncomes.Variable = expenseVm.Variable;
                updatedIncomes.Cost = expenseVm.Cost;

                updatedIncomes.AccountNumber = expenseVm.AccountNumber;
                updatedIncomes.Creditor = expenseVm.Creditor;

                try
                {
                    _context.Update(updatedIncomes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomesExists(updatedIncomes.Id))
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

            return View(expenseVm);
            
        }

        // GET: Incomes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Income expense = await _context.tbl_incomes.Where(e => e.Id == id).FirstAsync();
            IncomesDeleteVm expenseDelete = new IncomesDeleteVm
            {
                Id = expense.Id,

                Name = expense.Name,
                Life = expense.Life,
                Type = expense.Type,
                Variable = expense.Variable,
                Cost = expense.Cost
            };

            return View(expenseDelete);
        }

        // POST: Incomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var expense = await _context.tbl_incomes.FindAsync(id);
            _context.tbl_incomes.Remove(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncomesExists(long id)
        {
            return _context.tbl_incomes.Any(e => e.Id == id);
        }
    }
}
