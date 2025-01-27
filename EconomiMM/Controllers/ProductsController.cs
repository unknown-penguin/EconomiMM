﻿using System;
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
    public class ProductsController : Controller
    {
        private readonly EconomiMMContext _context;

        public ProductsController(EconomiMMContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
              return _context.Orders != null ? 
                          View(await _context.Orders.ToListAsync()) :
                          Problem("Entity set 'EconomiMMContext.Orders'  is null.");
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var product = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,orderNumber,temperature,jointShape,jointType,withLiner,mainPartLength1,mainPartDiameter,mainPartLength2,mainPartWidth,flangeWidth,koef,siliconeTubes,linerPartLength,linerPartHeight,linerPartWidth,linerPartBindingWidth,mainPartWorkPrice,linerPartWorkPrice,TotalPrice")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
       

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var product = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'EconomiMMContext.Orders'  is null.");
            }
            var product = await _context.Orders.FindAsync(id);
            if (product != null)
            {
                _context.Orders.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int? id)
        {
          return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
