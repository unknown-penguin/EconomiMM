using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EconomiMM.Data;
using EconomiMM.Models;

namespace EconomiMM.Controllers
{
    public class SellHistoriesController : Controller
    {
        private readonly EconomiMMContext _context;

        public SellHistoriesController(EconomiMMContext context)
        {
            _context = context;
        }

        // GET: SellHistories
        public async Task<IActionResult> Index()
        {
            
              return _context.SellHistory != null ? 
                          View(await _context.SellHistory.Include(sh => sh.material).ToListAsync()) :
                          Problem("Entity set 'EconomiMMContext.SellHistory'  is null.");
        }

        // GET: SellHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SellHistory == null)
            {
                return NotFound();
            }

            var sellHistory = await _context.SellHistory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sellHistory == null)
            {
                return NotFound();
            }

            return View(sellHistory);
        }

        // GET: SellHistories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SellHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Count,Sold")] SellHistory sellHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sellHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sellHistory);
        }

        // GET: SellHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SellHistory == null)
            {
                return NotFound();
            }

            var sellHistory = await _context.SellHistory.FindAsync(id);
            if (sellHistory == null)
            {
                return NotFound();
            }
            return View(sellHistory);
        }

        // POST: SellHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Count,Sold")] SellHistory sellHistory)
        {
            if (id != sellHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sellHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellHistoryExists(sellHistory.Id))
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
            return View(sellHistory);
        }

        // GET: SellHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SellHistory == null)
            {
                return NotFound();
            }

            var sellHistory = await _context.SellHistory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sellHistory == null)
            {
                return NotFound();
            }

            return View(sellHistory);
        }

        // POST: SellHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SellHistory == null)
            {
                return Problem("Entity set 'EconomiMMContext.SellHistory'  is null.");
            }
            var sellHistory = await _context.SellHistory.FindAsync(id);
            if (sellHistory != null)
            {
                _context.SellHistory.Remove(sellHistory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellHistoryExists(int id)
        {
          return (_context.SellHistory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
