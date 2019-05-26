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
    public class SecurityController : Controller
    {
        private readonly FinaContext _context;

        public SecurityController(FinaContext context)
        {
            _context = context;
        }

        // GET: Security
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_security.ToListAsync());
        }

        // GET: Security/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var security = await _context.tbl_security
                .FirstOrDefaultAsync(m => m.Id == id);
            if (security == null)
            {
                return NotFound();
            }

            return View(security);
        }

        // GET: Security/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Security/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,Type,Monthly,StartDate,Description,AccountNumber,Id")] security security)
        {
            if (ModelState.IsValid)
            {
                _context.Add(security);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(security);
        }

        // GET: Security/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var security = await _context.tbl_security.FindAsync(id);
            if (security == null)
            {
                return NotFound();
            }
            return View(security);
        }

        // POST: Security/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Amount,Type,Monthly,StartDate,Description,AccountNumber,Id")] security security)
        {
            if (id != security.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(security);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!securityExists(security.Id))
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
            return View(security);
        }

        // GET: Security/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var security = await _context.tbl_security
                .FirstOrDefaultAsync(m => m.Id == id);
            if (security == null)
            {
                return NotFound();
            }

            return View(security);
        }

        // POST: Security/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var security = await _context.tbl_security.FindAsync(id);
            _context.tbl_security.Remove(security);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool securityExists(long id)
        {
            return _context.tbl_security.Any(e => e.Id == id);
        }
    }
}
