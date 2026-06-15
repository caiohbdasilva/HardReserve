using System.Collections.Generic;
using System.Threading.Tasks;
using HardReserve.Contexts;
using HardReserve.Interfaces;
using HardReserve.Models;
using Microsoft.EntityFrameworkCore;

namespace HardReserve.Repositores 
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly HardReserveDbContext _context;

        public ReservaRepository(HardReserveDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reserva>> ListarTodasReservasAsync()
        {
            // O Include garante que a propriedade "Usuario" venha preenchida para a View mostrar o nome do aluno
            return await _context.Reserva
                .Include(r => r.Usuario)
                .ToListAsync();
        }
    }
}