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
    public class IncomeController : Controller
    {
        private readonly FinaContext _context;

        public IncomeController(FinaContext context)
        {
            _context = context;
        }

        // GET: Income
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_incomes.ToListAsync());
        }

        // GET: Income/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomes = await _context.tbl_incomes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incomes == null)
            {
                return NotFound();
            }

            return View(incomes);
        }

        // GET: Income/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Income/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Total,TotalWorkHours,Id")] incomes incomes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incomes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(incomes);
        }

        // GET: Income/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomes = await _context.tbl_incomes.FindAsync(id);
            if (incomes == null)
            {
                return NotFound();
            }
            return View(incomes);
        }

        // POST: Income/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Total,TotalWorkHours,Id")] incomes incomes)
        {
            if (id != incomes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incomes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!incomesExists(incomes.Id))
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
            return View(incomes);
        }

        // GET: Income/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomes = await _context.tbl_incomes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incomes == null)
            {
                return NotFound();
            }

            return View(incomes);
        }

        // POST: Income/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var incomes = await _context.tbl_incomes.FindAsync(id);
            _context.tbl_incomes.Remove(incomes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool incomesExists(long id)
        {
            return _context.tbl_incomes.Any(e => e.Id == id);
        }
    }
}
