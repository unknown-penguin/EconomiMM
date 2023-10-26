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
    public class FlangeMaterialsController : Controller
    {
        private readonly EconomiMMContext _context;

        public FlangeMaterialsController(EconomiMMContext context)
        {
            _context = context;
        }

        // GET: FlangeMaterials
        public async Task<IActionResult> Index()
        {
              return _context.FlangeMaterials != null ? 
                          View(await _context.FlangeMaterials.ToListAsync()) :
                          Problem("Entity set 'EconomiMMContext.FlangeMaterials'  is null.");
        }

        // GET: FlangeMaterials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FlangeMaterials == null)
            {
                return NotFound();
            }

            var flangeMaterial = await _context.FlangeMaterials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flangeMaterial == null)
            {
                return NotFound();
            }

            return View(flangeMaterial);
        }

        // GET: FlangeMaterials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FlangeMaterials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Thickness,Price")] FlangeMaterial flangeMaterial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flangeMaterial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flangeMaterial);
        }

        // GET: FlangeMaterials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FlangeMaterials == null)
            {
                return NotFound();
            }

            var flangeMaterial = await _context.FlangeMaterials.FindAsync(id);
            if (flangeMaterial == null)
            {
                return NotFound();
            }
            return View(flangeMaterial);
        }

        // POST: FlangeMaterials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Thickness,Price")] FlangeMaterial flangeMaterial)
        {
            if (id != flangeMaterial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flangeMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlangeMaterialExists(flangeMaterial.Id))
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
            return View(flangeMaterial);
        }

        // GET: FlangeMaterials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FlangeMaterials == null)
            {
                return NotFound();
            }

            var flangeMaterial = await _context.FlangeMaterials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flangeMaterial == null)
            {
                return NotFound();
            }

            return View(flangeMaterial);
        }

        // POST: FlangeMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FlangeMaterials == null)
            {
                return Problem("Entity set 'EconomiMMContext.FlangeMaterials'  is null.");
            }
            var flangeMaterial = await _context.FlangeMaterials.FindAsync(id);
            if (flangeMaterial != null)
            {
                _context.FlangeMaterials.Remove(flangeMaterial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlangeMaterialExists(int id)
        {
          return (_context.FlangeMaterials?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
