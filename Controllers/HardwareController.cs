using Microsoft.AspNetCore.Mvc;
using HardReserve.Interfaces;
using HardReserve.Models;
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

        private bool UsuarioEhTecnico()
        {
            return HttpContext.Session.GetString("UsuarioRole") == "T";
        }

        public async Task<IActionResult> Index()
        {
            var listaHardwares = await _hardwareService.BuscarHardwareComCatAsync();

            return View(listaHardwares);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            if (HttpContext.Session.GetString("UsuarioId") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (!UsuarioEhTecnico())
            {
                TempData["Erro"] = "Acesso restrito: apenas técnicos podem cadastrar hardwares.";
                return RedirectToAction("Index");
            }

            return View(new Hardware());
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Hardware hardware, IFormFile? FotoHardware)
        {
            if (HttpContext.Session.GetString("UsuarioId") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (!UsuarioEhTecnico())
            {
                TempData["Erro"] = "Acesso restrito: apenas técnicos podem cadastrar hardwares.";
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View(hardware);
            }

            await _hardwareService.CadastrarHardwareAsync(hardware, FotoHardware);

            TempData["Sucesso"] = $"Hardware \"{hardware.Nome}\" cadastrado com sucesso!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            if (HttpContext.Session.GetString("UsuarioId") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (!UsuarioEhTecnico())
            {
                TempData["Erro"] = "Acesso restrito: apenas técnicos podem editar hardwares.";
                return RedirectToAction("Index");
            }

            var hardware = await _hardwareService.BuscarHardwarePorIdAsync(id);
            if (hardware == null)
            {
                TempData["Erro"] = "Hardware não encontrado.";
                return RedirectToAction("Index");
            }

            return View("Cadastrar", hardware);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Hardware hardware, IFormFile? FotoHardware)
        {
            if (HttpContext.Session.GetString("UsuarioId") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (!UsuarioEhTecnico())
            {
                TempData["Erro"] = "Acesso restrito: apenas técnicos podem editar hardwares.";
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View("Cadastrar", hardware);
            }

            await _hardwareService.AtualizarHardwareAsync(hardware, FotoHardware);

            TempData["Sucesso"] = $"Hardware \"{hardware.Nome}\" atualizado com sucesso!";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            if (HttpContext.Session.GetString("UsuarioId") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (!UsuarioEhTecnico())
            {
                TempData["Erro"] = "Acesso restrito: apenas técnicos podem excluir hardwares.";
                return RedirectToAction("Index");
            }

            var hardware = await _hardwareService.BuscarHardwarePorIdAsync(id);
            if (hardware == null)
            {
                TempData["Erro"] = "Hardware não encontrado.";
                return RedirectToAction("Index");
            }

            var excluido = await _hardwareService.ExcluirHardwareAsync(id);
            if (!excluido)
            {
                TempData["Erro"] = $"Não é possível excluir \"{hardware.Nome}\": ele está vinculado a uma ou mais reservas.";
                return RedirectToAction("Index");
            }

            TempData["Sucesso"] = $"Hardware \"{hardware.Nome}\" excluído com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
