using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Hardware>> ListarHardwaresDisponiveisAsync()
        {
            return await _reservaRepository.BuscarHardwaresDisponiveisAsync();
        }

        public async Task<(bool ok, string? erro, int reservaId)> CriarReservaAsync(Reserva reserva, Dictionary<int, int> itens)
        {

            var disponiveis = (await _reservaRepository.BuscarHardwaresDisponiveisAsync()).ToList();

            foreach (var item in itens)
            {
                var hardware = disponiveis.FirstOrDefault(h => h.Id == item.Key);

                if (hardware == null)
                    return (false, "Um dos hardwares não está mais disponível. Revise seu carrinho.", 0);

                if (item.Value < 1)
                    return (false, $"Quantidade inválida para \"{hardware.Nome}\".", 0);

                if (item.Value > hardware.QuantidadeDisponivel)
                    return (false, $"Só há {hardware.QuantidadeDisponivel} unidade(s) disponível(is) de \"{hardware.Nome}\".", 0);
            }

            reserva.StatusReserva = "PE";
            reserva.Quantidade = itens.Values.Sum();
            reserva.Protocolo = DateTime.Now.ToString("ddMMyyHHmmss");

            await _reservaRepository.CriarReservaAsync(reserva, itens);

            return (true, null, reserva.Id);
        }

        public async Task<Reserva?> BuscarReservaPorIdAsync(int id)
        {
            return await _reservaRepository.BuscarReservaPorIdAsync(id);
        }

        public async Task<IEnumerable<Hardware>> BuscarHardwaresDaReservaAsync(int reservaId)
        {
            return await _reservaRepository.BuscarHardwaresDaReservaAsync(reservaId);
        }
    }
}
