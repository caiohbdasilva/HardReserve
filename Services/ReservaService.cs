using System.Collections.Generic;
using System.Threading.Tasks;
using HardReserve.Interfaces;
using HardReserve.Models;

namespace HardReserve.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;

        public ReservaService(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public async Task<IEnumerable<Reserva>> ListarReservasAsync()
        {
            return await _reservaRepository.ListarTodasReservasAsync();
        }
    }
}