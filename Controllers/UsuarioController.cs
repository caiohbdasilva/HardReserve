using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HardReserve.Interfaces;

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
        public IActionResult Logar(string email, string senha)
        {
            var usuarioLogado = _service.AutenticarUsuario(email, senha);

            if (usuarioLogado == null)
            {
                TempData["Erro"] = "E-mail ou senha inválidos, ou usuário inativo!";
                return RedirectToAction("Index", "Login");
            }

            HttpContext.Session.SetString("UsuarioId", usuarioLogado.Id.ToString());
            HttpContext.Session.SetString("UsuarioNome", usuarioLogado.Nome);

            HttpContext.Session.SetString("UsuarioRole", usuarioLogado.Role.ToString());

            return RedirectToAction("Index", "Hardware");
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
