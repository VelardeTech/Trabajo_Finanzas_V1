﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("UsuarioLogueado");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string correo, string contrasena)
        {
            var cliente = _context.Clientes.FirstOrDefault(x => x.CorreoElectronico == correo && x.Contrasena == contrasena);
            if (cliente != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, cliente.CorreoElectronico),
                    new Claim(ClaimTypes.Hash, cliente.Contrasena)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                HttpContext.SignInAsync(claimsPrincipal);
                HttpContext.Session.SetString("UsuarioLogueado", "true");
                return RedirectToAction("PlanDePagos", "Home");
            }
            else
            {
                ViewData["UsuarioLogeado"] = false;
                ViewBag.Error = "Credenciales inválidas";
                return View();
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