using HardReserve.Interfaces; 
using HardReserve.Models;   

namespace HardReserve.Services

{
    // A classe assina o contrato da IUsuarioService que você acabou de criar
    public class UsuarioService : IUsuarioService
    {
        // A service não mexe direto no banco. Ela pede para o repositório fazer isso.
        private readonly IUsuarioRepository _repository;

        // Injeção de Dependência: o C# vai injetar o UsuarioRepository aqui dentro automaticamente
        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public Usuario AutenticarUsuario(string email, string senha)
        {
            // 1. Pedimos ao repositório para ir ao banco buscar o usuário com esse e-mail e senha
            var usuario = _repository.BuscarPorEmailSenha(email, senha);

            // 2. REGRA DE NEGÓCIO EXTRA: Se o usuário existir no banco, mas o status dele 
            // for 'I' (Inativo), nós barramos o login aqui na camada de serviço!
            if (usuario != null && usuario.StatusUsuario == "I")
            {
                return null; // Retorna nulo, fingindo que não achou, pois a conta está bloqueada
            }

            // 3. Se passou pela regra ou se realmente for nulo, devolvemos o resultado
            return usuario;
        }
    }
}