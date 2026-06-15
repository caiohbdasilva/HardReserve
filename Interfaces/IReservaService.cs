using System.Collections.Generic;
using System.Threading.Tasks;
using HardReserve.Models;

namespace HardReserve.Interfaces
{
    public interface IReservaService
    {
        // Esse método precisa retornar a lista de Reservas para bater com o Controller
        Task<IEnumerable<Reserva>> ListarReservasAsync();
    }
}