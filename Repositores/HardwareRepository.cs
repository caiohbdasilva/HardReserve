using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardReserve.Contexts;
using HardReserve.Interfaces;
using HardReserve.Models;
using Microsoft.EntityFrameworkCore;

namespace HardReserve.Repositores
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
    }
}