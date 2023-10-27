using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EconomiMM.Data;
using EconomiMM.Models;
using EconomiMM.ViewModels;

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
                          View(await _context.FlangeMaterials.Include(t => t.Type).ToListAsync()) :
                          Problem("Entity set 'EconomiMMContext.FlangeMaterials'  is null.");
        }

        // GET: FlangeMaterials/Create

        public IActionResult Create()
        {
            var materialTypesList = _context.JointMaterialTypes.ToList();
            var viewmodel = new CalcMaterialViewModel<FlangeMaterial>
            {
                Material = new FlangeMaterial(),
                MaterialTypes = new SelectList(materialTypesList, nameof(ExpansionJointMaterialType.Id), nameof(ExpansionJointMaterialType.Name))
            };
            return View(viewmodel);
        }

        // POST: FlangeMaterials/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CalcMaterialViewModel<FlangeMaterial> viewmodel)
        {

            var materialTypesList = _context.JointMaterialTypes.ToList();
            viewmodel.MaterialTypes = new SelectList(materialTypesList, nameof(FlangeMaterial.Id), nameof(FlangeMaterial.Name));
            var selectedType = viewmodel.Material.Type.Id;
            viewmodel.Material.Type = _context.JointMaterialTypes.Where(m => m.Id == selectedType).First();
            if (ModelState.IsValid)
            {
                _context.Add(viewmodel.Material);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(viewmodel);
        }

        // GET: FlangeMaterials/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FlangeMaterials == null)
            {
                return NotFound();
            }

            var expansionJointMaterial = await _context.FlangeMaterials
                .Include(ejm => ejm.Type)
                .FirstOrDefaultAsync(ejm => ejm.Id == id);

            if (expansionJointMaterial == null)
            {
                return NotFound();
            }

            var materialTypesList = _context.JointMaterialTypes.ToList();
            var jointMaterialViewModel = new CalcMaterialViewModel<FlangeMaterial>
            {
                Material = expansionJointMaterial,
                MaterialTypes = new SelectList(materialTypesList, nameof(ExpansionJointMaterialType.Id), nameof(ExpansionJointMaterialType.Name))
            };

            return View(jointMaterialViewModel);
        }

        // POST: FlangeMaterials/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CalcMaterialViewModel<FlangeMaterial> viewmodel)
        {
            if (id != viewmodel.Material.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewmodel.Material);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlangeMaterialExists(viewmodel.Material.Id))
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
            return View(viewmodel);
        }

        // GET: FlangeMaterials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FlangeMaterials == null)
            {
                return NotFound();
            }

            var flangeMaterial = await _context.FlangeMaterials.Include(t => t.Type)
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
