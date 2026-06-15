using System.Collections.Generic;
using System.Threading.Tasks;
using HardReserve.Models;

namespace HardReserve.Interfaces
{
    public interface IReservaRepository
    {
        Task<IEnumerable<Reserva>> ListarTodasReservasAsync();
    }
}