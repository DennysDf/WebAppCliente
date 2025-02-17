using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppCliente.Models;
using WebAppCliente.Services.Interfaces;

namespace WebAppCliente.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _ClienteService;        
        private string token = string.Empty;

        public ClienteController(IClienteService ClienteService)
        {
            _ClienteService = ClienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteViewModel>>> Index()
        {           
            var result = await _ClienteService.GetClientes(ObtemTokenJwt());

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> NovoCliente() => View();
        
        [HttpPost]
        public async Task<ActionResult<ClienteViewModel>> NovoCliente(ClienteViewModel ClienteVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _ClienteService.CriaCliente(ClienteVM, ObtemTokenJwt());

                if (result != null)
                    return RedirectToAction(nameof(Index));
            }else
            {
                View("Error");
            }

            return View(ClienteVM);
        }

        [HttpGet]
        public async Task<IActionResult> DetalhesCliente(int id)
        {
            var Cliente = await _ClienteService.GetClientePorId(id, ObtemTokenJwt());

            if (Cliente is null)
                return View("Error");

            return View(Cliente);
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarCliente(int id)
        {
            var result = await _ClienteService.GetClientePorId(id, ObtemTokenJwt());

            if (result is null)
                return View("Error");

            return View(result);
        }
        [HttpPost]
        public async Task<ActionResult<ClienteViewModel>> AtualizarCliente(int id, ClienteViewModel ClienteVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _ClienteService.AtualizaCliente(id, ClienteVM, ObtemTokenJwt());

                if (result)
                    return RedirectToAction(nameof(Index));
            }
            return View(ClienteVM);
        }
        [HttpGet]
        public async Task<ActionResult> DeletarCliente(int id)
        {
            var result = await _ClienteService.GetClientePorId(id, ObtemTokenJwt());

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost(), ActionName("DeletarCliente")]
        public async Task<IActionResult> DeletaConfirmado(int id)
        {
            var result = await _ClienteService.DeletaCliente(id, ObtemTokenJwt());

            if (result)
                return RedirectToAction("Index");

            return View("Error");
        }

        private string ObtemTokenJwt()
        {
            if (HttpContext.Request.Cookies.ContainsKey("X-Access-Token"))
                token = HttpContext.Request.Cookies["X-Access-Token"].ToString();

            return token;
        }
    }
}
