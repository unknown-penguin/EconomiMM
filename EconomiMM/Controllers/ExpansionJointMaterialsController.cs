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
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
                        View(await _context.ExpansionJointsMaterials.Include(t => t.Type).ToListAsync()) :
                        Problem("Entity set 'EconomiMMContext.ExpansionJointsMaterials'  is null.");
        }

        // GET: ExpansionJointMaterials/Create
        public IActionResult Create()
        {
            var materialTypesList = _context.JointMaterialTypes.ToList();
            var viewmodel = new CalcMaterialViewModel<ExpansionJointMaterial>
            {
                Material = new ExpansionJointMaterial(),
                MaterialTypes = new SelectList(materialTypesList, nameof(ExpansionJointMaterialType.Id), nameof(ExpansionJointMaterialType.Name))
            };
            return View(viewmodel);
        }

        // POST: ExpansionJointMaterials/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CalcMaterialViewModel<ExpansionJointMaterial> viewmodel)
        {

            var materialTypesList = _context.JointMaterialTypes.ToList();
            viewmodel.MaterialTypes = new SelectList(materialTypesList, nameof(ExpansionJointMaterialType.Id), nameof(ExpansionJointMaterialType.Name));
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

        // GET: ExpansionJointMaterials/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ExpansionJointsMaterials == null)
            {
                return NotFound();
            }

            var expansionJointMaterial = await _context.ExpansionJointsMaterials
                .Include(ejm => ejm.Type)
                .FirstOrDefaultAsync(ejm => ejm.Id == id);

            if (expansionJointMaterial == null)
            {
                return NotFound();
            }

            var materialTypesList = _context.JointMaterialTypes.ToList();
            var jointMaterialViewModel = new CalcMaterialViewModel<ExpansionJointMaterial>
            {
                Material = expansionJointMaterial,
                MaterialTypes = new SelectList(materialTypesList, nameof(ExpansionJointMaterialType.Id), nameof(ExpansionJointMaterialType.Name))
            };
            
            return View(jointMaterialViewModel);
        }

        // POST: ExpansionJointMaterials/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CalcMaterialViewModel<ExpansionJointMaterial> viewmodel)
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
                    if (!ExpansionJointMaterialExists(viewmodel.Material.Id))
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

        // GET: ExpansionJointMaterials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ExpansionJointsMaterials == null)
            {
                return NotFound();
            }

            var expansionJointMaterial = await _context.ExpansionJointsMaterials.Include(t => t.Type)
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
