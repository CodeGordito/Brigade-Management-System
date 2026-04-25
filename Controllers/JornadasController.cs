using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestorBrigadasComunitarias.Data;
using GestorBrigadasComunitarias.Models;

namespace GestorBrigadasComunitarias.Controllers
{
    public class JornadasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JornadasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jornadas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Jornadas.Include(j => j.Zona);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Jornadas/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var jornada = await _context.Jornadas
                    .Include(j => j.Zona)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (jornada == null)
                {
                    return NotFound();
                }

                return View(jornada);
            }

        // GET: Jornadas/Create
        public IActionResult Create()
        {
            ViewData["ZonaId"] = new SelectList(_context.Zonas, "Id", "Nombre");
            return View();
        }

        // POST: Jornadas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Fecha,Lugar,Descripcion,ZonaId")] Jornada jornada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jornada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ZonaId"] = new SelectList(_context.Zonas, "Id", "Nombre", jornada.ZonaId);
            return View(jornada);
        }

        // GET: Jornadas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jornada = await _context.Jornadas.FindAsync(id);
            if (jornada == null)
            {
                return NotFound();
            }
            ViewData["ZonaId"] = new SelectList(_context.Zonas, "Id", "Nombre", jornada.ZonaId);
            return View(jornada);
        }

        // POST: Jornadas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Fecha,Lugar,Descripcion,ZonaId")] Jornada jornada)
        {
            if (id != jornada.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jornada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JornadaExists(jornada.Id))
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
            ViewData["ZonaId"] = new SelectList(_context.Zonas, "Id", "Nombre", jornada.ZonaId);
            return View(jornada);
        }

        // GET: Jornadas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jornada = await _context.Jornadas
                .Include(j => j.Zona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jornada == null)
            {
                return NotFound();
            }

            return View(jornada);
        }

        // POST: Jornadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jornada = await _context.Jornadas.FindAsync(id);
            if (jornada != null)
            {
                _context.Jornadas.Remove(jornada);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JornadaExists(int id)
        {
            return _context.Jornadas.Any(e => e.Id == id);
        }
    }
}
