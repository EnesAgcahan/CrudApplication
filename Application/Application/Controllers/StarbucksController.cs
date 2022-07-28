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
    public class StarbucksController : Controller
    {
        private readonly ApplicationContext _context;

        public StarbucksController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Starbucks
        public async Task<IActionResult> Index()
        {
              return _context.Starbucks != null ? 
                          View(await _context.Starbucks.ToListAsync()) :
                          Problem("Entity set 'ApplicationContext.Starbucks'  is null.");
        }

        // GET: Starbucks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Starbucks == null)
            {
                return NotFound();
            }

            var starbucks = await _context.Starbucks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (starbucks == null)
            {
                return NotFound();
            }

            return View(starbucks);
        }

        // GET: Starbucks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Starbucks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Create(IFormCollection form)
        {
            Starbucks starbucks = new Starbucks();
            starbucks.Ad = form["Ad"];
            starbucks.Soyad = form["Soyad"];
            starbucks.TcKimlikNo = form["TcKimlikNo"];
            string d = form["DogumTarihi"];
            DateTime DogumTarihi = DateTime.Parse(d, System.Globalization.CultureInfo.InvariantCulture);
            starbucks.DogumTarihi = DogumTarihi;
            if (starbucks.TcKimlikDogrula(starbucks).Result)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(starbucks);
                    await _context.SaveChangesAsync();
                    ViewBag.HataMesaji = "Başarılı";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.HataMesaji = "Model state is not valid";
                    return View(starbucks);
                }


            }
            else
            {
                ViewBag.HataMesaji = "Hata.Lütfen girdiğiniz bilgileri kontrol ediniz.";
                return View(starbucks);
            }



        }

        // GET: Starbucks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Starbucks == null)
            {
                return NotFound();
            }

            var starbucks = await _context.Starbucks.FindAsync(id);
            if (starbucks == null)
            {
                return NotFound();
            }
            return View(starbucks);
        }

        // POST: Starbucks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Soyad,TcKimlikNo,DogumTarihi")] Starbucks starbucks)
        {
            if (id != starbucks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(starbucks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StarbucksExists(starbucks.Id))
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
            return View(starbucks);
        }

        // GET: Starbucks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Starbucks == null)
            {
                return NotFound();
            }

            var starbucks = await _context.Starbucks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (starbucks == null)
            {
                return NotFound();
            }

            return View(starbucks);
        }

        // POST: Starbucks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Starbucks == null)
            {
                return Problem("Entity set 'ApplicationContext.Starbucks'  is null.");
            }
            var starbucks = await _context.Starbucks.FindAsync(id);
            if (starbucks != null)
            {
                _context.Starbucks.Remove(starbucks);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StarbucksExists(int id)
        {
          return (_context.Starbucks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
