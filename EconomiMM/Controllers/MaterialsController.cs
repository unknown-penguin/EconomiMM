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
    public class MaterialsController : Controller
    {
        private readonly EconomiMMContext _context;

        public MaterialsController(EconomiMMContext context)
        {
            _context = context;
        }

        // GET: Materials
        public async Task<IActionResult> Index(float thickness)
        {
            List<Material>? materials = null;
            if (thickness > 0)
            {
                materials = await _context.Material.Where(m => m.Thickness == thickness).ToListAsync();
            }
            else
            {
                materials = await _context.Material.ToListAsync();

            }
            return _context.Material != null ?
                        View(materials) :
                        Problem("Entity set 'EconomiMMContext.Material'  is null.");
        }

        // GET: Materials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Material == null)
            {
                return NotFound();
            }

            var material = await _context.Material
                .FirstOrDefaultAsync(m => m.Id == id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // GET: Materials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Materials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Thickness,Size,Count,Reserved,Price")] Material material)
        {
            if (ModelState.IsValid)
            {
                _context.Add(material);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "MaterialTypes");
            }
            return View(material);
        }

        // GET: Materials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Material == null)
            {
                return NotFound();
            }

            var material = await _context.Material.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            return View(material);
        }

        // POST: Materials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Thickness,Size,Count,Reserved,Price")] Material material)
        {
            if (id != material.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(material);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialExists(material.Id))
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
            return View(material);
        }

        // GET: Materials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Material == null)
            {
                return NotFound();
            }

            var material = await _context.Material
                .FirstOrDefaultAsync(m => m.Id == id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Material == null)
            {
                return Problem("Entity set 'EconomiMMContext.Material'  is null.");
            }
            var material = await _context.Material.FindAsync(id);
            if (material != null)
            {
                _context.Material.Remove(material);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "MaterialTypes");
        }

        private bool MaterialExists(int id)
        {
            return (_context.Material?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        public async Task<IActionResult> Sell(int? id)
        {
            if (id == null || _context.Material == null)
            {
                return NotFound();
            }

            var material = await _context.Material.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            return View(material);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sell(int id, [Bind("Id,Name,Thickness,Size,Count,Reserved,Price,Sold")] Material material)
        {
            if (id != material.Id)
            {
                return NotFound();
            }
            var newCount = material.Count - material.Sold;
            if (newCount >= 0)
            {
                var count = material.Count;
                var sold = material.Sold;
                SellHistory sellHistory = new SellHistory
                {
                    material = material,
                    Count = count,
                    Sold = sold
                };

                _context.SellHistory.Add(sellHistory);

                material.Count = material.Count - material.Sold;
            }
            else
            {

            }
            material.Sold = 0;

            //add to sell history

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(material);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialExists(material.Id))
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
            return View(material);
        }
    }
}
