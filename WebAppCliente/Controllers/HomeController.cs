using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppCliente.Models;
using WebAppCliente.Services.Interfaces;

namespace WebAppCliente.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAutenticacao _autenticacaoService;

        public HomeController(IAutenticacao autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Login Inválido....");
                return View(model);
            }

            var result = await _autenticacaoService.AutenticaUsuario(model);

            if (result is null)
            {
                ModelState.AddModelError(string.Empty, "Login Inválido....");
                return View(model);
            }

            Response.Cookies.Append("X-Access-Token", result.Token, new CookieOptions()
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });
            return RedirectToAction("Index","Cliente");

        }

    }
}
