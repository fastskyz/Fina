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
    public class ExpenseController : Controller
    {
        private readonly FinaContext _context;

        public ExpenseController(FinaContext context)
        {
            _context = context;
        }

        // GET: Expense
        public async Task<IActionResult> Index()
        {
            long id = 1;
            User user = await _context.tbl_users.Where(u => u.Id == id).FirstAsync();
            ExpensesOverviewVm expensesOverviewVm = new ExpensesOverviewVm();

            expensesOverviewVm.Expenses = _context.Entry(user).Collection(u => u.Expenses).Query().AsEnumerable();
            expensesOverviewVm.Total = expensesOverviewVm.Expenses.Count();

            expensesOverviewVm.Life = expensesOverviewVm.Expenses.Where(e => e.Life).Count();
            expensesOverviewVm.Variable = expensesOverviewVm.Expenses.Where(e => e.Variable).Count();

            return View(expensesOverviewVm);
        }

        // GET: Expense/Add
        public IActionResult Add()
        {
            ExpenseAddVm expenseAddVm = new ExpenseAddVm();

            return View(expenseAddVm);
        }

        // POST: Expense/Add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // ModelState.IsValid

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Name,Life,Type,Variable,Cost,AccountNumber,Creditor,expenseTypes")] ExpenseAddVm expenseVm)
        {
            if (ModelState.IsValid)
            {
                var fk = await _context.tbl_users.Where(u => u.Id == 1).FirstAsync();

                Expense newExpense = new Expense {
                    FK = fk,
                    Name = expenseVm.Name,
                    AccountNumber = expenseVm.AccountNumber,
                    Life = expenseVm.Life,
                    Type = expenseVm.Type,
                    Variable = expenseVm.Variable,
                    Cost = expenseVm.Cost,
                    Creditor = expenseVm.Creditor
                };

                _context.Add(newExpense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenseVm);
        }



        // GET: Expense/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            Expense currentExpense  = await _context.tbl_expenses.Where(e => e.Id == id).FirstAsync();
            ExpenseAddVm expenseAddVm = new ExpenseAddVm
            {
                Name = currentExpense.Name,
                Life = currentExpense.Life,
                Type = currentExpense.Type,
                Variable = currentExpense.Variable,
                Cost = currentExpense.Cost,

                AccountNumber = currentExpense.AccountNumber,
                Creditor = currentExpense.Creditor
            };

            return View(expenseAddVm);
        }

        // POST: Expense/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Life,Type,Variable,Cost,AccountNumber,Creditor,expenseTypes")] ExpenseAddVm expenseVm)
        {
            if (ModelState.IsValid)
            {
                Expense updatedExpense = await _context.tbl_expenses.Where(e => e.Id == id).FirstAsync();

                // Copy over properties
                updatedExpense.Name = expenseVm.Name;
                updatedExpense.Life = expenseVm.Life;
                updatedExpense.Type = expenseVm.Type;
                updatedExpense.Variable = expenseVm.Variable;
                updatedExpense.Cost = expenseVm.Cost;

                updatedExpense.AccountNumber = expenseVm.AccountNumber;
                updatedExpense.Creditor = expenseVm.Creditor;

                try
                {
                    _context.Update(updatedExpense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(updatedExpense.Id))
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

        // GET: Expense/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Expense expense = await _context.tbl_expenses.Where(e => e.Id == id).FirstAsync();
            ExpenseDeleteVm expenseDelete = new ExpenseDeleteVm
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

        // POST: Expense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var expense = await _context.tbl_expenses.FindAsync(id);
            _context.tbl_expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(long id)
        {
            return _context.tbl_expenses.Any(e => e.Id == id);
        }
    }
}
