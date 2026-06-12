using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; 
using HardReserve.Interfaces;     
using System.Threading.Tasks; // Adicionado para usar o Task

namespace HardReserve.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpPost]
        // Mudamos para public async Task<IActionResult>
        public async Task<IActionResult> Logar(string email, string senha)
        {
            // Adicionamos o "await" antes de chamar a service
            var usuarioLogado = await _service.AutenticarUsuario(email, senha);

            if (usuarioLogado == null)
            {
                TempData["Erro"] = "E-mail ou senha inválidos, ou usuário inativo!";
                return RedirectToAction("Index", "Login");
            }

            HttpContext.Session.SetString("UsuarioId", usuarioLogado.Id.ToString());
            HttpContext.Session.SetString("UsuarioNome", usuarioLogado.Nome);
            HttpContext.Session.SetString("UsuarioRole", usuarioLogado.Role);

            return RedirectToAction("Index", "Hardware");
        }
    }
}