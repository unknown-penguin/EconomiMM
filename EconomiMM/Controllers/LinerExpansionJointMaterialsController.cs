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
    public class LinerExpansionJointMaterialsController : Controller
    {
        private readonly EconomiMMContext _context;

        public LinerExpansionJointMaterialsController(EconomiMMContext context)
        {
            _context = context;
        }

        // GET: LinerExpansionJointMaterials
        public async Task<IActionResult> Index()
        {
              return _context.LinerMaterials != null ? 
                          View(await _context.LinerMaterials.ToListAsync()) :
                          Problem("Entity set 'EconomiMMContext.LinerMaterials'  is null.");
        }

        // GET: LinerExpansionJointMaterials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LinerMaterials == null)
            {
                return NotFound();
            }

            var linerExpansionJointMaterial = await _context.LinerMaterials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (linerExpansionJointMaterial == null)
            {
                return NotFound();
            }

            return View(linerExpansionJointMaterial);
        }

        // GET: LinerExpansionJointMaterials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LinerExpansionJointMaterials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Thickness,Price,PartOfLiner")] LinerExpansionJointMaterial linerExpansionJointMaterial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(linerExpansionJointMaterial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(linerExpansionJointMaterial);
        }

        // GET: LinerExpansionJointMaterials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LinerMaterials == null)
            {
                return NotFound();
            }

            var linerExpansionJointMaterial = await _context.LinerMaterials.FindAsync(id);
            if (linerExpansionJointMaterial == null)
            {
                return NotFound();
            }
            return View(linerExpansionJointMaterial);
        }

        // POST: LinerExpansionJointMaterials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Thickness,Price,PartOfLiner")] LinerExpansionJointMaterial linerExpansionJointMaterial)
        {
            if (id != linerExpansionJointMaterial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(linerExpansionJointMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinerExpansionJointMaterialExists(linerExpansionJointMaterial.Id))
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
            return View(linerExpansionJointMaterial);
        }

        // GET: LinerExpansionJointMaterials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LinerMaterials == null)
            {
                return NotFound();
            }

            var linerExpansionJointMaterial = await _context.LinerMaterials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (linerExpansionJointMaterial == null)
            {
                return NotFound();
            }

            return View(linerExpansionJointMaterial);
        }

        // POST: LinerExpansionJointMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LinerMaterials == null)
            {
                return Problem("Entity set 'EconomiMMContext.LinerMaterials'  is null.");
            }
            var linerExpansionJointMaterial = await _context.LinerMaterials.FindAsync(id);
            if (linerExpansionJointMaterial != null)
            {
                _context.LinerMaterials.Remove(linerExpansionJointMaterial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LinerExpansionJointMaterialExists(int id)
        {
          return (_context.LinerMaterials?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
