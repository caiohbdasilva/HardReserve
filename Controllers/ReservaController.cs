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

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UsuarioId") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var listaReservas = await _reservaService.ListarReservasAsync();
            return View(listaReservas);
        }
    }
} 