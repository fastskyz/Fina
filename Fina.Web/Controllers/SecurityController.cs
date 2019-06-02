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
    public class SecuritiesController : Controller
    {
        private readonly FinaContext _context;




        public SecuritiesController(FinaContext context)
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




        // GET: Securities
        public async Task<IActionResult> Index()
        {
            if (IsLoggedIn())
            {
                string data = HttpContext.Session.GetString("User");
                UserSessionModel userSession = JsonConvert.DeserializeObject<UserSessionModel>(data);

                var user = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == userSession.Id);

                decimal TotalSaved = 0;
                decimal MonthlySaved = 0;

                SecuritiesOverviewVm securitiesOverviewVm = new SecuritiesOverviewVm();
                securitiesOverviewVm.Securities = _context.Entry(user).Collection(u => u.Securities).Query().AsEnumerable();
                securitiesOverviewVm.nSecurities = securitiesOverviewVm.Securities.Count();

                foreach ( var sec in securitiesOverviewVm.Securities )
                {
                    TotalSaved += sec.Amount;
                }

                securitiesOverviewVm.Total = TotalSaved;

                foreach (var sec in securitiesOverviewVm.Securities)
                {
                    TotalSaved += sec.Monthly;
                }

                securitiesOverviewVm.Monthly = MonthlySaved;

                return View(securitiesOverviewVm);
            }

            return RedirectToAction("Login", "User");
        }











        // GET: Securities/Add
        public IActionResult Add()
        {
            if (IsLoggedIn())
            {
                SecurityAddVm securitiesAddVm = new SecurityAddVm();

                return View(securitiesAddVm);
            }

            return RedirectToAction("Login", "User");
        }

        // POST: Securities/Add

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Name,Longterm,Monthly,Type,Description,AccountNumber,StartDate")] SecurityAddVm securityVm)
        {
            if (ModelState.IsValid)
            {
                string data = HttpContext.Session.GetString("User");
                UserSessionModel userSession = JsonConvert.DeserializeObject<UserSessionModel>(data);

                var fk = await _context.tbl_users.FirstOrDefaultAsync(u => u.Id == userSession.Id);

                Security newSecurities = new Security {
                    FK = fk,

                    Name = securityVm.Name,
                    Longterm = securityVm.Longterm,
                    Monthly = securityVm.Monthly,
                    Type = securityVm.Type,

                    Description = securityVm.Description,
                    AccountNumber = securityVm.AccountNumber,
                    StartDate = securityVm.StartDate
                };

                _context.Add(newSecurities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(securityVm);
        }














        // GET: Securities/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (IsLoggedIn())
            {
                Security currentSecurities = await _context.tbl_securities.Where(e => e.Id == id).FirstAsync();
                SecurityAddVm securityAddVm = new SecurityAddVm
                {
                    Name = currentSecurities.Name,
                    Longterm = currentSecurities.Longterm,
                    Monthly = currentSecurities.Monthly,
                    Type = currentSecurities.Type,

                    Description = currentSecurities.Description,
                    AccountNumber = currentSecurities.AccountNumber,
                    StartDate = currentSecurities.StartDate
                };

                return View(securityAddVm);
            }

            return RedirectToAction("Login", "User");
            
        }

        // POST: Securities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Longterm,Monthly,Type,Description,AccountNumber,StartDate")] SecurityAddVm securityVm)
        {
            if (ModelState.IsValid)
            {
                Security updatedSecurity = await _context.tbl_securities.Where(e => e.Id == id).FirstAsync();

                // Copy over properties
                updatedSecurity.Name = securityVm.Name;
                updatedSecurity.Monthly = securityVm.Monthly;
                updatedSecurity.Longterm = securityVm.Longterm;
                updatedSecurity.Type = securityVm.Type;

                updatedSecurity.Description = securityVm.Description;
                updatedSecurity.AccountNumber = securityVm.AccountNumber;
                updatedSecurity.StartDate = securityVm.StartDate;

                try
                {
                    _context.Update(updatedSecurity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecuritiesExists(updatedSecurity.Id))
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

            return View(securityVm);
            
        }

















        // GET: Securities/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (IsLoggedIn())
            {
                if (id == null)
                {
                    return NotFound();
                }

                Security security = await _context.tbl_securities.Where(e => e.Id == id).FirstAsync();
                SecuritiesDeleteVm securityDelete = new SecuritiesDeleteVm
                {
                    Id = security.Id,

                    Name = security.Name,
                    Amount = security.Amount,
                    Monthly = security.Monthly,
                    Longterm = security.Longterm,
                    Type = security.Type,
                };

                return View(securityDelete);
            }

            return RedirectToAction("Login", "User");

            
        }

        // POST: Securities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var security = await _context.tbl_securities.FindAsync(id);
            _context.tbl_securities.Remove(security);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecuritiesExists(long id)
        {
            return _context.tbl_securities.Any(e => e.Id == id);
        }
    }
}
