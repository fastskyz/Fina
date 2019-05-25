using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fina.Lib.Database;

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
            return View(await _context.tbl_expenses.ToListAsync());
        }

        // GET: Expense/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenses = await _context.tbl_expenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenses == null)
            {
                return NotFound();
            }

            return View(expenses);
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
        public async Task<IActionResult> Create([Bind("Total,LifeFunds,Id")] expenses expenses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenses);
        }

        // GET: Expense/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenses = await _context.tbl_expenses.FindAsync(id);
            if (expenses == null)
            {
                return NotFound();
            }
            return View(expenses);
        }

        // POST: Expense/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Total,LifeFunds,Id")] expenses expenses)
        {
            if (id != expenses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!expensesExists(expenses.Id))
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
            return View(expenses);
        }

        // GET: Expense/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenses = await _context.tbl_expenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenses == null)
            {
                return NotFound();
            }

            return View(expenses);
        }

        // POST: Expense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var expenses = await _context.tbl_expenses.FindAsync(id);
            _context.tbl_expenses.Remove(expenses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool expensesExists(long id)
        {
            return _context.tbl_expenses.Any(e => e.Id == id);
        }
    }
}
