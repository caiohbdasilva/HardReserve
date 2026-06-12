using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Para usar a Sessão (Session)
using HardReserve.Interfaces;     // Para enxergar o IUsuarioService

namespace HardReserve.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _service;

        // Injeção de Dependência da sua Service
        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Logar(string email, string senha)
        {
            var usuarioLogado = _service.AutenticarUsuario(email, senha);

            if (usuarioLogado == null)
            {
                TempData["Erro"] = "E-mail ou senha inválidos, ou usuário inativo!";
                return RedirectToAction("Index", "Login"); 
            }

            // Salvando os dados na Sessão
            HttpContext.Session.SetString("UsuarioId", usuarioLogado.Id.ToString());
            HttpContext.Session.SetString("UsuarioNome", usuarioLogado.Nome);
            // HttpContext.Session.SetString("UsuarioRole", usuarioLogado.Role);

            return RedirectToAction("Index", "Hardware");
        }
    }
}