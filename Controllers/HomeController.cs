using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Trabajo_Finanzas_V1.Models;

namespace Trabajo_Finanzas_V1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BdFianzasContext _context;

        public HomeController(ILogger<HomeController> logger, BdFianzasContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult PlanDePagos()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Desarrolladores()
        {
            return View();
        }

        public IActionResult Somos()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> Login(int id, string correo, string contrasena)
        {
            try
            {
                var cliente = _context.Clientes.FirstOrDefault(x => x.IdUser == id && x.CorreoElectronico == correo && x.Contrasena == contrasena);
                return Task.FromResult<IActionResult>(RedirectToAction("PlanDePagos", "Home"));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return Task.FromResult<IActionResult>(View());
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Nombre,Apellido,Dni,Telefono,CorreoElectronico,Contrasena")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}