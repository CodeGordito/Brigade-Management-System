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
    public class ParticipacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParticipacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Participacions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Participaciones
                .Include(p => p.Voluntario)
                .Include(p => p.Jornada);

            return View(await applicationDbContext.ToListAsync());
        }
        

        // GET: Participacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participacion = await _context.Participaciones
                .Include(p => p.Jornada)
                .Include(p => p.Voluntario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participacion == null)
            {
                return NotFound();
            }

            return View(participacion);
        }

        // GET: Participacions/Create
        public IActionResult Create()
        {
            ViewData["JornadaId"] = new SelectList(_context.Jornadas, "Id", "Lugar");
            ViewData["VoluntarioId"] = new SelectList(_context.Voluntarios, "Id", "Nombre");
            return View();
        }

        // POST: Participacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VoluntarioId,JornadaId,Asistio,HorasTrabajadas,Observaciones")] Participacion participacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(participacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JornadaId"] = new SelectList(_context.Jornadas, "Id", "Lugar", participacion.JornadaId);
            ViewData["VoluntarioId"] = new SelectList(_context.Voluntarios, "Id", "Apellido", participacion.VoluntarioId);
            return View(participacion);
        }

        // GET: Participacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participacion = await _context.Participaciones.FindAsync(id);
            if (participacion == null)
            {
                return NotFound();
            }
            ViewData["JornadaId"] = new SelectList(_context.Jornadas, "Id", "Lugar", participacion.JornadaId);
            ViewData["VoluntarioId"] = new SelectList(_context.Voluntarios, "Id", "Apellido", participacion.VoluntarioId);
            return View(participacion);
        }

        // POST: Participacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VoluntarioId,JornadaId,Asistio,HorasTrabajadas,Observaciones")] Participacion participacion)
        {
            if (id != participacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipacionExists(participacion.Id))
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
            ViewData["JornadaId"] = new SelectList(_context.Jornadas, "Id", "Lugar", participacion.JornadaId);
            ViewData["VoluntarioId"] = new SelectList(_context.Voluntarios, "Id", "Apellido", participacion.VoluntarioId);
            return View(participacion);
        }

        // GET: Participacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participacion = await _context.Participaciones
                .Include(p => p.Jornada)
                .Include(p => p.Voluntario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participacion == null)
            {
                return NotFound();
            }

            return View(participacion);
        }

        // POST: Participacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var participacion = await _context.Participaciones.FindAsync(id);
            if (participacion != null)
            {
                _context.Participaciones.Remove(participacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipacionExists(int id)
        {
            return _context.Participaciones.Any(e => e.Id == id);
        }
    }
}
