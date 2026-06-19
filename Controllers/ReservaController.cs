using Microsoft.AspNetCore.Mvc;
using HardReserve.Interfaces;
using Microsoft.AspNetCore.Http; 
using System.Threading.Tasks;   

namespace HardReserve.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IReservaService _reservaService;

        public ReservaController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        // 1. ROTA: /Reserva/Listagem (Chama a tela Listagem.cshtml)
        public async Task<IActionResult> Listagem()
        {
            if (HttpContext.Session.GetString("UsuarioId") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var listaReservas = await _reservaService.ListarReservasAsync();
            return View(listaReservas); // Como o método chama "Listagem", ele busca automaticamente o Listagem.cshtml
        }

        // 2. ROTA: /Reserva/Solicitar (Chama a tela Solicitar.cshtml)
        [HttpGet]
        public IActionResult Solicitar()
        {
            if (HttpContext.Session.GetString("UsuarioId") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View(); // Como o método chama "Solicitar", ele busca automaticamente o Solicitar.cshtml
        }
    }
}