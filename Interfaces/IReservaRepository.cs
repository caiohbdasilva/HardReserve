using System.Collections.Generic;
using System.Threading.Tasks;
using HardReserve.Models;

namespace HardReserve.Interfaces
{
    public interface IReservaRepository
    {
        Task<IEnumerable<Reserva>> ListarTodasReservasAsync();

        Task<IEnumerable<Hardware>> BuscarHardwaresDisponiveisAsync();

        Task<Reserva?> BuscarReservaPorIdAsync(int id);

        Task<IEnumerable<Hardware>> BuscarHardwaresDaReservaAsync(int reservaId);

        Task<Dictionary<int, int>> ObterReservadoPorHardwareAsync();

        Task CriarReservaAsync(Reserva reserva, Dictionary<int, int> itens);

        Task AtualizarStatusAsync(int id, string novoStatus);
    }
}
