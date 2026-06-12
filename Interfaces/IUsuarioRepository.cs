using HardReserve.Models;

namespace HardReserve.Interfaces
{
    public interface IUsuarioRepository
    {
        // Aqui apenas declaramos a intenção do método. 
        // Ele promete retornar um objeto "Usuario" se achar alguém com esse e-mail e senha.
        Usuario BuscarPorEmailSenha(string email, string senha);
    }
}