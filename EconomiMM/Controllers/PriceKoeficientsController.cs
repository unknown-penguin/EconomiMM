 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EconomiMM.Data;
using EconomiMM.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.Differencing;

namespace EconomiMM.Controllers
{
    public class PriceKoeficientsController : Controller
    {
        private readonly EconomiMMContext _context;

        public PriceKoeficientsController(EconomiMMContext context)
        {
            _context = context;
        }

        // GET: PriceKoeficients
        public async Task<IActionResult> Index()
        {
            return _context.PriceKoeficients != null ?
                        View(await _context.PriceKoeficients.ToListAsync()) :
                        Problem("Entity set 'EconomiMMContext.PriceKoeficients'  is null.");
        }

        // GET: PriceKoeficients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PriceKoeficients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Manufacturer,OurPriceForSheetKoef,OurPriceForSqMetreKoef,DealerPriceForSheetKoef,DealerPriceForSqMetreKoef")] PriceKoeficients priceKoeficients)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priceKoeficients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(priceKoeficients);
        }

        // GET: PriceKoeficients/Edit
        public async Task<IActionResult> Edit()
        {
            return _context.PriceKoeficients != null ?
                         View(await _context.PriceKoeficients.ToListAsync()) :
                         Problem("Entity set 'EconomiMMContext.PriceKoeficients'  is null.");
        }

        // POST: PriceKoeficients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string manufacturer, PriceKoeficients priceKoeficients)
        {



            if (ModelState.IsValid)
            {
                try
                {
                    var materialTypes = await _context.MaterialType.Where(m => m.Manufacturer == manufacturer).ToListAsync();
                    foreach (var materialType in materialTypes)
                    {
                        var materials = await _context.Material.Where(m => m.Name == materialType.Name).ToListAsync();
                        foreach (var material in materials)
                        {
                            material.OurPriceForSheet = (int)CalcNewPrice(material.Price, priceKoeficients.OurPriceForSheetKoef);
                            material.OurPriceForSqMetre = (int)CalcNewPriceBySize(material.Price, priceKoeficients.OurPriceForSqMetreKoef, GetSizesOfMaterial(material.Size));
                            material.DealerPriceForSheet = (int)CalcNewPrice(material.Price, priceKoeficients.DealerPriceForSheetKoef);
                            material.DealerPriceForSqMetre = (int)CalcNewPriceBySize(material.Price, priceKoeficients.DealerPriceForSqMetreKoef, GetSizesOfMaterial(material.Size));
                            _context.Update(material);
                        }
                    }
                    _context.Update(priceKoeficients);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceKoeficientsExists(priceKoeficients.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return RedirectToAction("Index", "Materials");
        }
        private (float, float) GetSizesOfMaterial(string materialSizeString)
        {
            if (string.IsNullOrWhiteSpace(materialSizeString))
            {
                return (1000, 1000);
            }

            string pattern = @"\b(\d{3,4})х(\d{3,4})\b";
            Regex regex = new Regex(pattern);

            Match match = regex.Match(materialSizeString);

            if (match.Success)
            {
                float width = float.Parse(match.Groups[1].Value) / 1000;
                float height = float.Parse(match.Groups[2].Value) / 1000;
                return (width, height);
            }
            else
            {
                return (1000, 1000);
            }

        }
        private float CalcNewPriceBySize(float prevPrice, float koef, (float width, float height) size)
        {
            if (prevPrice <= 0)
            {
                return 0;
            }
            float newPrice = prevPrice / (size.height * size.width) * koef;
            return (float)Math.Round(newPrice / 100.0) * 100;
        }

        // GET: PriceKoeficients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PriceKoeficients == null)
            {
                return NotFound();
            }

            var priceKoeficients = await _context.PriceKoeficients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priceKoeficients == null)
            {
                return NotFound();
            }

            return View(priceKoeficients);
        }

        // POST: PriceKoeficients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PriceKoeficients == null)
            {
                return Problem("Entity set 'EconomiMMContext.PriceKoeficients'  is null.");
            }
            var priceKoeficients = await _context.PriceKoeficients.FindAsync(id);
            if (priceKoeficients != null)
            {
                _context.PriceKoeficients.Remove(priceKoeficients);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriceKoeficientsExists(int id)
        {
            return (_context.PriceKoeficients?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private float CalcNewPrice(float prevPrice, float koef)
        {
            if (prevPrice <= 0)
            {
                return 0;
            }
            float newPrice = prevPrice * koef;
            return (float)Math.Round(newPrice / 100.0) * 100;
        }

    }
}
