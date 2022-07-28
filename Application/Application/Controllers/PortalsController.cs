using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Application.Data;
using Application.Models;

namespace Application.Controllers
{
    public class PortalsController : Controller
    {
        private readonly ApplicationContext _context;

        public PortalsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Portals
        public async Task<IActionResult> Index()
        {
              return _context.Portal != null ? 
                          View(await _context.Portal.ToListAsync()) :
                          Problem("Entity set 'ApplicationContext.Portal'  is null.");
        }

        // GET: Portals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Portal == null)
            {
                return NotFound();
            }

            var portal = await _context.Portal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portal == null)
            {
                return NotFound();
            }

            return View(portal);
        }

        // GET: Portals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Portals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,Soyad,TcKimlikNo,DogumTarihi")] Portal portal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(portal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(portal);
        }

        // GET: Portals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Portal == null)
            {
                return NotFound();
            }

            var portal = await _context.Portal.FindAsync(id);
            if (portal == null)
            {
                return NotFound();
            }
            return View(portal);
        }

        // POST: Portals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Soyad,TcKimlikNo,DogumTarihi")] Portal portal)
        {
            if (id != portal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortalExists(portal.Id))
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
            return View(portal);
        }

        // GET: Portals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Portal == null)
            {
                return NotFound();
            }

            var portal = await _context.Portal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portal == null)
            {
                return NotFound();
            }

            return View(portal);
        }

        // POST: Portals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Portal == null)
            {
                return Problem("Entity set 'ApplicationContext.Portal'  is null.");
            }
            var portal = await _context.Portal.FindAsync(id);
            if (portal != null)
            {
                _context.Portal.Remove(portal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortalExists(int id)
        {
          return (_context.Portal?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
