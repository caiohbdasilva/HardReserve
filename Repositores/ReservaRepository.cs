using System.Collections.Generic;
using System.Linq;
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
            return await _context.Reserva
                .Include(r => r.Usuario)
                .OrderByDescending(r => r.Id)
                .ToListAsync();
        }

        public async Task<Dictionary<int, int>> ObterReservadoPorHardwareAsync()
        {
            var query = from hr in _context.Hardware_Reserva
                        join r in _context.Reserva on hr.Reserva_Id equals r.Id
                        where r.StatusReserva != "CA" && r.StatusReserva != "DE"
                        group hr by hr.Hardware_Id into g
                        select new { HardwareId = g.Key, Total = g.Sum(x => x.Quantidade) };

            return await query.ToDictionaryAsync(x => x.HardwareId, x => x.Total);
        }

        public async Task<IEnumerable<Hardware>> BuscarHardwaresDisponiveisAsync()
        {
            var reservado = await ObterReservadoPorHardwareAsync();

            var hardwares = await _context.Hardware
                .Where(h => h.Status == "disponivel")
                .OrderBy(h => h.Nome)
                .ToListAsync();

            foreach (var hw in hardwares)
            {
                var jaReservado = reservado.ContainsKey(hw.Id) ? reservado[hw.Id] : 0;
                hw.QuantidadeDisponivel = hw.Quantidade_Total - jaReservado;
            }

            return hardwares.Where(h => h.QuantidadeDisponivel > 0).ToList();
        }

        public async Task<Reserva?> BuscarReservaPorIdAsync(int id)
        {
            return await _context.Reserva
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Hardware>> BuscarHardwaresDaReservaAsync(int reservaId)
        {

            var itens = await _context.Hardware_Reserva
                .Where(hr => hr.Reserva_Id == reservaId)
                .ToListAsync();

            var hardwareIds = itens.Select(i => i.Hardware_Id).ToList();

            var hardwares = await _context.Hardware
                .Where(h => hardwareIds.Contains(h.Id))
                .ToListAsync();

            foreach (var hw in hardwares)
            {
                var item = itens.FirstOrDefault(i => i.Hardware_Id == hw.Id);
                hw.QuantidadeNaReserva = item?.Quantidade ?? 0;
            }

            return hardwares;
        }

        public async Task CriarReservaAsync(Reserva reserva, Dictionary<int, int> itens)
        {

            await _context.Reserva.AddAsync(reserva);
            await _context.SaveChangesAsync();

            foreach (var item in itens)
            {
                await _context.Hardware_Reserva.AddAsync(new Hardware_Reserva
                {
                    Reserva_Id = reserva.Id,
                    Hardware_Id = item.Key,
                    Quantidade = item.Value
                });
            }

            await _context.SaveChangesAsync();
        }
    
        public async Task AtualizarStatusAsync(int id, string novoStatus)
        {
            var reserva = await _context.Reserva.FirstOrDefaultAsync(r => r.Id == id);
            if (reserva != null)
            {
                reserva.StatusReserva = novoStatus;
                await _context.SaveChangesAsync();
            }
        }
    }
}
