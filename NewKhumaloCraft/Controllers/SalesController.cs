using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewKhumaloCraft.Data;
using NewKhumaloCraft.Models;

namespace NewKhumaloCraft.Controllers
{
    
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Sales
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sales.ToListAsync());
        }

        // GET: Sales/Details/5
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesEntity = await _context.Sales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesEntity == null)
            {
                return NotFound();
            }

            return View(salesEntity);
        }

        // GET: Sales/Create
        [Authorize(Roles = "User,Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Mobile,Email")] SalesEntity salesEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesEntity);
        }

        // GET: Sales/Edit/5
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesEntity = await _context.Sales.FindAsync(id);
            if (salesEntity == null)
            {
                return NotFound();
            }
            return View(salesEntity);
        }

        [Authorize(Roles = "User,Admin")]
        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Mobile,Email")] SalesEntity salesEntity)
        {
            if (id != salesEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesEntityExists(salesEntity.Id))
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
            return View(salesEntity);
        }

        // GET: Sales/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesEntity = await _context.Sales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesEntity == null)
            {
                return NotFound();
            }

            return View(salesEntity);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesEntity = await _context.Sales.FindAsync(id);
            if (salesEntity != null)
            {
                _context.Sales.Remove(salesEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesEntityExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}
