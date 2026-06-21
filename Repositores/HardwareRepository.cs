using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardReserve.Contexts;
using HardReserve.Interfaces;
using HardReserve.Models;
using Microsoft.EntityFrameworkCore;

namespace HardReserve.Repository
{
    public class HardwareRepository : IHardwareRepository
    {
        private readonly HardReserveDbContext _context;

        public HardwareRepository(HardReserveDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hardware>> BuscarHardwareAsync()
        {
            return await _context.Hardware
                .ToListAsync();
        }

        public async Task CadastrarHardwareAsync(Hardware hardware)
        {
            await _context.Hardware.AddAsync(hardware);
            await _context.SaveChangesAsync();
        }

        public async Task<Hardware?> BuscarHardwarePorIdAsync(int id)
        {
            return await _context.Hardware.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task AtualizarHardwareAsync(Hardware hardware)
        {
            _context.Hardware.Update(hardware);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExcluirHardwareAsync(int id)
        {
            var emUso = await _context.Hardware_Reserva.AnyAsync(hr => hr.Hardware_Id == id);
            if (emUso)
            {
                return false;
            }

            var hardware = await _context.Hardware.FirstOrDefaultAsync(h => h.Id == id);
            if (hardware == null)
            {
                return false;
            }

            _context.Hardware.Remove(hardware);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
