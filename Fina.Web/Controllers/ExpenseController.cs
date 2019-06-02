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
    public class ExpenseController : Controller
    {


        private readonly FinaContext _context;

        public ExpenseController(FinaContext context)
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





        // GET: Expense
        public async Task<IActionResult> Index()
        {
            if ( IsLoggedIn() )
            {
                string data = HttpContext.Session.GetString("User");
                UserSessionModel userSession = JsonConvert.DeserializeObject<UserSessionModel>(data);

                var user = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == userSession.Id);

                ExpensesOverviewVm expensesOverviewVm = new ExpensesOverviewVm();
                expensesOverviewVm.Expenses = _context.Entry(user).Collection(u => u.Expenses).Query().AsEnumerable();
                expensesOverviewVm.Total = expensesOverviewVm.Expenses.Count();
                expensesOverviewVm.Life = expensesOverviewVm.Expenses.Where(e => e.Life).Count();
                expensesOverviewVm.Variable = expensesOverviewVm.Expenses.Where(e => e.Variable).Count();

                return View(expensesOverviewVm);

            }

            return RedirectToAction("Login", "User");  
        }

        // GET: Expense/Add
        public IActionResult Add()
        {
            if (IsLoggedIn())
            {
                ExpenseAddVm expenseAddVm = new ExpenseAddVm();

                return View(expenseAddVm);
            }

            return RedirectToAction("Login", "User");
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
                string data = HttpContext.Session.GetString("User");
                UserSessionModel userSession = JsonConvert.DeserializeObject<UserSessionModel>(data);

                var fk = await _context.tbl_users.Where(u => u.Id == userSession.Id).FirstOrDefaultAsync();

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
            if (IsLoggedIn())
            {
                Expense currentExpense = await _context.tbl_expenses.Where(e => e.Id == id).FirstOrDefaultAsync();
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

            return RedirectToAction("Login", "User");

            
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
                Expense updatedExpense = await _context.tbl_expenses.Where(e => e.Id == id).FirstOrDefaultAsync();

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
            if (IsLoggedIn())
            {
                if (id == null)
                {
                    return NotFound();
                }

                Expense expense = await _context.tbl_expenses.Where(e => e.Id == id).FirstOrDefaultAsync();
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

            return RedirectToAction("Login", "User");

            
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
