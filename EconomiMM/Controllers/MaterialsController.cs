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
    public class MaterialsController : Controller
    {
        private readonly EconomiMMContext _context;

        public MaterialsController(EconomiMMContext context)
        {
            _context = context;
        }

        // GET: Materials
        [HttpGet]
        public async Task<IActionResult> Index(float? thickness, string name)
        {
            IQueryable<Material> materialsQuery = _context.Material.Include(c =>c.Colors).OrderBy(t => t.Name);
            ViewData["Thickness"] = 0;
            ViewData["Name"] = "";
            if (thickness.HasValue && thickness > 0)
            {
                ViewData["Thickness"] = thickness;
                materialsQuery = materialsQuery.Where(m => m.Thickness == thickness);
            }

            if (!string.IsNullOrEmpty(name))
            {
                ViewData["Name"] = name;
                materialsQuery = materialsQuery.Where(m => m.Name == name);
            }

            List<Material> materials = await materialsQuery.ToListAsync();

            return _context.Material != null ?
                View(materials) :
                Problem("Entity set 'EconomiMMContext.Material' is null.");
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
        [HttpGet]
        public async Task<IActionResult> Create(string? materialType)
        {
            var material = new Material();
            if (string.IsNullOrEmpty(materialType))
            {
                material.Name = string.Empty;
            }
            else
            {
                material.Name = materialType;
            }

            

            var colors = await _context.Colors.ToListAsync();
            string selectedColors = "";

            var editMaterialViewModel = new EditMaterialViewModel
            {
                Material = material,
                SelectedColorsId = selectedColors,
                Colors = colors
            };

            return View(editMaterialViewModel);
        }

        // POST: Materials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EditMaterialViewModel editMaterialViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Update the Material entity with the new Color associations
                    var material = editMaterialViewModel.Material;

                    var selectedColor = GetSelectedColorById(editMaterialViewModel.SelectedColorsId);

                    // Add new associations based on selectedColor
                    foreach (var color in selectedColor)
                    {
                        _context.Colors.Attach(color);
                        if (color != null)
                        {
                            material.Colors.Add(color);

                        }
                    }
                    _context.Add(material);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialExists(editMaterialViewModel.Material.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(editMaterialViewModel);
        }

        // GET: Materials/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Material == null)
            {
                return NotFound();
            }

            var material = await _context.Material.Include(m => m.Colors).FirstOrDefaultAsync(m => m.Id == id);
            var colors = await _context.Colors.ToListAsync();

            var manufacturer = _context.MaterialType.Where(mt => mt.Name == material.Name).FirstOrDefault().Manufacturer;
            var priceKoeficients = _context.PriceKoeficients.Where(pk => pk.Manufacturer == manufacturer).First();
            if (material == null)
            {
                return NotFound();
            }
            string selectedColors = "";
            foreach (var color in material.Colors)
            {
                selectedColors += color.Id.ToString() + ",";
            }
            var editMaterialViewModel = new EditMaterialViewModel
            {
                Material = material,
                SelectedColorsId = selectedColors,
                Colors = colors,
                PriceKoeficients = priceKoeficients
            };
            return View(editMaterialViewModel);
        }

        // POST: Materials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditMaterialViewModel editMaterialViewModel)
        {
            if (id != editMaterialViewModel.Material.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var material = _context.Material.Include(m => m.Colors).Where(m => m.Id == editMaterialViewModel.Material.Id).First();
                    _context.Entry(material).CurrentValues.SetValues(editMaterialViewModel.Material);
                    var selectedColor = GetSelectedColorById(editMaterialViewModel.SelectedColorsId);
                    

                    material.Colors.Clear();


                    foreach (var color in selectedColor)
                    {
                        _context.Colors.Attach(color);
                        if (color != null && !material.Colors.Contains(color))
                        {
                            material.Colors.Add(color);

                        }
                    }
                   
                    material.OurPriceForSheet = CalcNewPrice(material.Price, editMaterialViewModel.PriceKoeficients.OurPriceForSheetKoef); 
                    material.OurPriceForSqMetre = CalcNewPrice(material.Price, editMaterialViewModel.PriceKoeficients.OurPriceForSqMetreKoef);
                    material.DealerPriceForSheet = CalcNewPrice(material.Price, editMaterialViewModel.PriceKoeficients.DealerPriceForSheetKoef);
                    material.DealerPriceForSqMetre = CalcNewPrice(material.Price, editMaterialViewModel.PriceKoeficients.DealerPriceForSqMetreKoef);

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialExists(editMaterialViewModel.Material.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(editMaterialViewModel);
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
        public async Task<IActionResult> Sell(int id, Material material)
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

        private List<Color> GetSelectedColorById(string? ids)
        {
            if(ids != null && ids.EndsWith(","))
            {
                ids = ids.Trim(); // Remove leading and trailing whitespace
                ids = ids.Substring(0, ids.Length - 1); // Remove trailing comma
                List<int> idsList = ids.
                Split(',').
                Select(int.Parse).
                ToList();

                List<Color> colors = new List<Color>();
                foreach (int id in idsList)
                {
                    var selectedColor = _context.Colors.FirstOrDefault(c => c.Id == id);
                    colors.Add(selectedColor);
                }
                return colors;
            }
            return new List<Color>();


        }
        private int CalcNewPrice(int prevPrice, float koef)
        {
            if (prevPrice <= 0)
            {
                return 0;
            }

            var newPrice = prevPrice * koef;
            if(newPrice < 1000)
            {
                return (int)Math.Ceiling(newPrice / 10.0) * 10;
            }

            return (int)Math.Ceiling(newPrice / 100.0) * 100;
        }
    }
}
