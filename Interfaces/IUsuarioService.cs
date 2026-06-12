using HardReserve.Models;

namespace HardReserve.Interfaces
{
    public interface IUsuarioService
    {
        // A service vai receber o e-mail e senha da tela, processar e devolver o Usuário autenticado
        Usuario AutenticarUsuario(string email, string senha);
    }
}