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
            return View(await _context.tbl_jobs.ToListAsync());
        }

        // GET: Income/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobs = await _context.tbl_jobs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobs == null)
            {
                return NotFound();
            }

            return View(jobs);
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
        public async Task<IActionResult> Create([Bind("Income,Variable,WorkHours,Function,Company,StartDate,Id")] jobs jobs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobs);
        }

        // GET: Income/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobs = await _context.tbl_jobs.FindAsync(id);
            if (jobs == null)
            {
                return NotFound();
            }
            return View(jobs);
        }

        // POST: Income/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Income,Variable,WorkHours,Function,Company,StartDate,Id")] jobs jobs)
        {
            if (id != jobs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!jobsExists(jobs.Id))
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
            return View(jobs);
        }

        // GET: Income/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobs = await _context.tbl_jobs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobs == null)
            {
                return NotFound();
            }

            return View(jobs);
        }

        // POST: Income/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var jobs = await _context.tbl_jobs.FindAsync(id);
            _context.tbl_jobs.Remove(jobs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool jobsExists(long id)
        {
            return _context.tbl_jobs.Any(e => e.Id == id);
        }
    }
}
