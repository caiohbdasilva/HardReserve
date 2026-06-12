using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardReserve.Interfaces;
using HardReserve.Models;

namespace HardReserve.Services
{
    public class HardwareService : IHardwareService
    {
        private readonly IHardwareRepository _hardwareRepository;

        public HardwareService(IHardwareRepository hardwareRepository)
        {
            _hardwareRepository = hardwareRepository;
        }

        public async Task<IEnumerable<Hardware>> BuscarHardwareComCatAsync()
        {
            return await _hardwareRepository.BuscarHardwareAsync();
        }
    }
}