using System.Collections.Generic;
using System.Threading.Tasks;
using HardReserve.Models;

namespace HardReserve.Interfaces
{
    public interface IReservaService
    {
        Task<IEnumerable<Reserva>> ListarReservasAsync();

        Task<IEnumerable<Hardware>> ListarHardwaresDisponiveisAsync();

        Task<(bool ok, string? erro, int reservaId)> CriarReservaAsync(Reserva reserva, Dictionary<int, int> itens);

        Task<Reserva?> BuscarReservaPorIdAsync(int id);
        Task<IEnumerable<Hardware>> BuscarHardwaresDaReservaAsync(int reservaId);
    }
}
