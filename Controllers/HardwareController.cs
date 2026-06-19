using Microsoft.AspNetCore.Mvc;
using HardReserve.Interfaces;
using Microsoft.AspNetCore.Http; 
using System.Threading.Tasks;

namespace HardReserve.Controllers
{
    public class HardwareController : Controller
    {
        private readonly IHardwareService _hardwareService;

        public HardwareController(IHardwareService hardwareService)
        {
            _hardwareService = hardwareService;
        }

        // 1. ROTA: /Hardware (Exibe o Catálogo)
        public async Task<IActionResult> Index() 
        {
            if (HttpContext.Session.GetString("UsuarioId") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var listaHardwares = await _hardwareService.BuscarHardwareComCatAsync(); 

            return View(listaHardwares);
        }

        // 2. ROTA: /Hardware/Cadastrar (Abre a tela Cadastrar.cshtml)
        [HttpGet]
        public IActionResult Cadastrar()
        {
            // Proteção: Só acessa a tela de cadastro se estiver logado
            if (HttpContext.Session.GetString("UsuarioId") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View(); // Busca automaticamente o arquivo Views/Hardware/Cadastrar.cshtml
        }
    }
}