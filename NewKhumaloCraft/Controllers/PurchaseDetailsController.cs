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
    public class PurchaseDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PurchaseDetails
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.PurchaseDetails.ToListAsync());
        }

        // GET: PurchaseDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDetails = await _context.PurchaseDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseDetails == null)
            {
                return NotFound();
            }

            return View(purchaseDetails);
        }

        // GET: PurchaseDetails/Create
        [Authorize(Roles = "Admin,User")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: PurchaseDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,City,Country,Artwork")] PurchaseDetails purchaseDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchaseDetails);
        }

        // GET: PurchaseDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDetails = await _context.PurchaseDetails.FindAsync(id);
            if (purchaseDetails == null)
            {
                return NotFound();
            }
            return View(purchaseDetails);
        }

        // POST: PurchaseDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,City,Country,Artwork")] PurchaseDetails purchaseDetails)
        {
            if (id != purchaseDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseDetailsExists(purchaseDetails.Id))
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
            return View(purchaseDetails);
        }

        // GET: PurchaseDetails/Delete/5
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDetails = await _context.PurchaseDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseDetails == null)
            {
                return NotFound();
            }

            return View(purchaseDetails);
        }

        // POST: PurchaseDetails/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseDetails = await _context.PurchaseDetails.FindAsync(id);
            if (purchaseDetails != null)
            {
                _context.PurchaseDetails.Remove(purchaseDetails);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseDetailsExists(int id)
        {
            return _context.PurchaseDetails.Any(e => e.Id == id);
        }
    }
}
