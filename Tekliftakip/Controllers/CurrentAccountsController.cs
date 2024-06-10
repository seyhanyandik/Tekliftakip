using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tekliftakip.Data;
using Tekliftakip.Models;

namespace Tekliftakip.Controllers
{
    public class CurrentAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CurrentAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CurrentAccounts
        public async Task<IActionResult> Index()
        {
              return _context.CurrentAccounts != null ? 
                          View(await _context.CurrentAccounts.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CurrentAccounts'  is null.");
        }

        // GET: CurrentAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CurrentAccounts == null)
            {
                return NotFound();
            }

            var currentAccount = await _context.CurrentAccounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (currentAccount == null)
            {
                return NotFound();
            }

            return View(currentAccount);
        }

        // GET: CurrentAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CurrentAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,CustomerId,AccountCode,AccountName,TaxNumber")] CurrentAccount currentAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(currentAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(currentAccount);
        }

        // GET: CurrentAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CurrentAccounts == null)
            {
                return NotFound();
            }

            var currentAccount = await _context.CurrentAccounts.FindAsync(id);
            if (currentAccount == null)
            {
                return NotFound();
            }
            return View(currentAccount);
        }

        // POST: CurrentAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,CustomerId,AccountCode,AccountName,TaxNumber")] CurrentAccount currentAccount)
        {
            if (id != currentAccount.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(currentAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrentAccountExists(currentAccount.AccountId))
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
            return View(currentAccount);
        }

        // GET: CurrentAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CurrentAccounts == null)
            {
                return NotFound();
            }

            var currentAccount = await _context.CurrentAccounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (currentAccount == null)
            {
                return NotFound();
            }

            return View(currentAccount);
        }

        // POST: CurrentAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CurrentAccounts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CurrentAccounts'  is null.");
            }
            var currentAccount = await _context.CurrentAccounts.FindAsync(id);
            if (currentAccount != null)
            {
                _context.CurrentAccounts.Remove(currentAccount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurrentAccountExists(int id)
        {
          return (_context.CurrentAccounts?.Any(e => e.AccountId == id)).GetValueOrDefault();
        }
    }
}
