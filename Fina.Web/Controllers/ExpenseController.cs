using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fina.Lib.Database;
using Fina.Web.Models;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> Index(long? id)
        {
            ExpensesOverviewVm expensesOvervieVm = new ExpensesOverviewVm();

            var user = await _context.tbl_users
                .FirstOrDefaultAsync(m => m.Id == id);


            return View();
        }

        // GET: Expense/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var single_expense = await _context.tbl_single_expenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (single_expense == null)
            {
                return NotFound();
            }

            return View(single_expense);
        }

        // GET: Expense/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expense/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Life,Type,Variable,Cost,AccountNumber,Creditor,Id")] single_expense single_expense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(single_expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(single_expense);
        }

        // GET: Expense/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var single_expense = await _context.tbl_single_expenses.FindAsync(id);
            if (single_expense == null)
            {
                return NotFound();
            }
            return View(single_expense);
        }

        // POST: Expense/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Life,Type,Variable,Cost,AccountNumber,Creditor,Id")] single_expense single_expense)
        {
            if (id != single_expense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(single_expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!single_expenseExists(single_expense.Id))
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
            return View(single_expense);
        }

        // GET: Expense/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var single_expense = await _context.tbl_single_expenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (single_expense == null)
            {
                return NotFound();
            }

            return View(single_expense);
        }

        // POST: Expense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var single_expense = await _context.tbl_single_expenses.FindAsync(id);
            _context.tbl_single_expenses.Remove(single_expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool single_expenseExists(long id)
        {
            return _context.tbl_single_expenses.Any(e => e.Id == id);
        }
    }
}
