using HardReserve.Contexts; 
using HardReserve.Interfaces; 
using HardReserve.Models;
using System.Linq;

namespace HardReserve.Repositories
{
    // Aqui dizemos que a classe UsuarioRepository vai implementar (cumprir o contrato) da IUsuarioRepository
    public class UsuarioRepository : IUsuarioRepository
    {
        // Essa variável vai guardar a conexão com o banco de dados enquanto o repositório estiver ativo
        private readonly HardReserveDbContext _context;

        // Construtor: Aqui acontece a "Injeção de Dependência". 
        // O ASP.NET vai ler esse construtor e passar o "HardReserveDbContext" automaticamente para nós.
        public UsuarioRepository(HardReserveDbContext context)
        {
            _context = context;
        }

        // Aqui está a implementação real do método que você declarou na interface
        public Usuario BuscarPorEmailSenha(string email, string senha)
        {
            // _context.Usuarios acessa a tabela que o Caio mapeou.
            // .FirstOrDefault procura o primeiro usuário que bata com a condição (E-mail E Senha corretos).
            // Se encontrar, retorna o Usuário com todos os dados. Se não encontrar, retorna null.
            return _context.Usuarios
                .FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }
    }
}