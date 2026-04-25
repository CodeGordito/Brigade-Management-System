using GestorBrigadasComunitarias.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorBrigadasComunitarias.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalVoluntarios = await _context.Voluntarios.CountAsync();
            ViewBag.TotalZonas = await _context.Zonas.CountAsync();
            ViewBag.TotalJornadas = await _context.Jornadas.CountAsync();
            ViewBag.TotalParticipaciones = await _context.Participaciones.CountAsync();

            var jornadasRecientes = await _context.Jornadas
                .Include(j => j.Zona)
                .OrderByDescending(j => j.Fecha)
                .Take(5)
                .ToListAsync();

            return View(jornadasRecientes);
        }
    }
}