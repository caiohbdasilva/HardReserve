using Microsoft.AspNetCore.Mvc;
using HardReserve.Interfaces;

namespace HardReserve.Controllers
{
    public class HardwareController : Controller
    {
        private readonly IHardwareService _hardwareService;

        public HardwareController(IHardwareService hardwareService)
        {
            _hardwareService = hardwareService;
        }

        // 1. Adicionado "async" aqui
        public async Task<IActionResult> Index() 
        {
            if (HttpContext.Session.GetString("UsuarioId") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            // 2. Mudou o nome do método e adicionou o "await" na frente
            var listaHardwares = await _hardwareService.BuscarHardwareComCatAsync(); 

            return View(listaHardwares);
        }
    }
}