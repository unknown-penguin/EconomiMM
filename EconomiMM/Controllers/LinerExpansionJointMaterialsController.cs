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
                          View(await _context.LinerMaterials.Include(t => t.Type).ToListAsync()) :
                          Problem("Entity set 'EconomiMMContext.LinerMaterials'  is null.");
        }

        // GET: LinerExpansionJointMaterials/Create
        public IActionResult Create()
        {
            var materialTypesList = _context.JointMaterialTypes.ToList();
            var viewmodel = new CalcMaterialViewModel<LinerExpansionJointMaterial>
            {
                Material = new LinerExpansionJointMaterial(),
                MaterialTypes = new SelectList(materialTypesList, nameof(ExpansionJointMaterialType.Id), nameof(ExpansionJointMaterialType.Name))
            };
            return View(viewmodel);
        }

        // POST: FlangeMaterials/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CalcMaterialViewModel<LinerExpansionJointMaterial> viewmodel)
        {

            var materialTypesList = _context.JointMaterialTypes.ToList();
            viewmodel.MaterialTypes = new SelectList(materialTypesList, nameof(LinerExpansionJointMaterial.Id), nameof(LinerExpansionJointMaterial.Name));
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


        // GET: LinerExpansionJointMaterials/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LinerMaterials == null)
            {
                return NotFound();
            }

            var expansionJointMaterial = await _context.LinerMaterials
                .Include(ejm => ejm.Type)
                .FirstOrDefaultAsync(ejm => ejm.Id == id);

            if (expansionJointMaterial == null)
            {
                return NotFound();
            }

            var materialTypesList = _context.JointMaterialTypes.ToList();
            var jointMaterialViewModel = new CalcMaterialViewModel<LinerExpansionJointMaterial>
            {
                Material = expansionJointMaterial,
                MaterialTypes = new SelectList(materialTypesList, nameof(ExpansionJointMaterialType.Id), nameof(ExpansionJointMaterialType.Name))
            };

            return View(jointMaterialViewModel);
        }

        // POST: FlangeMaterials/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CalcMaterialViewModel<LinerExpansionJointMaterial> viewmodel)
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
                    if (!LinerExpansionJointMaterialExists(viewmodel.Material.Id))
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

        // GET: LinerExpansionJointMaterials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LinerMaterials == null)
            {
                return NotFound();
            }

            var linerExpansionJointMaterial = await _context.LinerMaterials.Include(t => t.Type)
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
