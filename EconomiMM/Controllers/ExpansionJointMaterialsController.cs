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
    public class ExpansionJointMaterialsController : Controller
    {
        private readonly EconomiMMContext _context;

        public ExpansionJointMaterialsController(EconomiMMContext context)
        {
            _context = context;
        }

        // GET: ExpansionJointMaterials
        public async Task<IActionResult> Index()
        {
              return _context.ExpansionJointsMaterials != null ? 
                          View(await _context.ExpansionJointsMaterials.ToListAsync()) :
                          Problem("Entity set 'EconomiMMContext.ExpansionJointsMaterials'  is null.");
        }

        // GET: ExpansionJointMaterials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ExpansionJointsMaterials == null)
            {
                return NotFound();
            }

            var expansionJointMaterial = await _context.ExpansionJointsMaterials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expansionJointMaterial == null)
            {
                return NotFound();
            }

            return View(expansionJointMaterial);
        }

        // GET: ExpansionJointMaterials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExpansionJointMaterials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Thickness,Price")] ExpansionJointMaterial expansionJointMaterial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expansionJointMaterial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expansionJointMaterial);
        }

        // GET: ExpansionJointMaterials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ExpansionJointsMaterials == null)
            {
                return NotFound();
            }

            var expansionJointMaterial = await _context.ExpansionJointsMaterials.FindAsync(id);
            if (expansionJointMaterial == null)
            {
                return NotFound();
            }
            return View(expansionJointMaterial);
        }

        // POST: ExpansionJointMaterials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Thickness,Price")] ExpansionJointMaterial expansionJointMaterial)
        {
            if (id != expansionJointMaterial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expansionJointMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpansionJointMaterialExists(expansionJointMaterial.Id))
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
            return View(expansionJointMaterial);
        }

        // GET: ExpansionJointMaterials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ExpansionJointsMaterials == null)
            {
                return NotFound();
            }

            var expansionJointMaterial = await _context.ExpansionJointsMaterials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expansionJointMaterial == null)
            {
                return NotFound();
            }

            return View(expansionJointMaterial);
        }

        // POST: ExpansionJointMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ExpansionJointsMaterials == null)
            {
                return Problem("Entity set 'EconomiMMContext.ExpansionJointsMaterials'  is null.");
            }
            var expansionJointMaterial = await _context.ExpansionJointsMaterials.FindAsync(id);
            if (expansionJointMaterial != null)
            {
                _context.ExpansionJointsMaterials.Remove(expansionJointMaterial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpansionJointMaterialExists(int id)
        {
          return (_context.ExpansionJointsMaterials?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
