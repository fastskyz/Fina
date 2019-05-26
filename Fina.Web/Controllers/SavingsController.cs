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
    public class SavingsController : Controller
    {
        private readonly FinaContext _context;

        public SavingsController(FinaContext context)
        {
            _context = context;
        }

        // GET: Savings
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_savings.ToListAsync());
        }

        // GET: Savings/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savings = await _context.tbl_savings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (savings == null)
            {
                return NotFound();
            }

            return View(savings);
        }

        // GET: Savings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Savings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,Longterm,Monthly,Name,Type,Description,EndDate,StartDate,AccountNumber,Id")] savings savings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(savings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(savings);
        }

        // GET: Savings/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savings = await _context.tbl_savings.FindAsync(id);
            if (savings == null)
            {
                return NotFound();
            }
            return View(savings);
        }

        // POST: Savings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Amount,Longterm,Monthly,Name,Type,Description,EndDate,StartDate,AccountNumber,Id")] savings savings)
        {
            if (id != savings.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(savings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!savingsExists(savings.Id))
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
            return View(savings);
        }

        // GET: Savings/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savings = await _context.tbl_savings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (savings == null)
            {
                return NotFound();
            }

            return View(savings);
        }

        // POST: Savings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var savings = await _context.tbl_savings.FindAsync(id);
            _context.tbl_savings.Remove(savings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool savingsExists(long id)
        {
            return _context.tbl_savings.Any(e => e.Id == id);
        }
    }
}
