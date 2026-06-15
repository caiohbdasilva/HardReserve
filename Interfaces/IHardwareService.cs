using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardReserve.Models;

namespace HardReserve.Interfaces
{
    public interface IHardwareService
    {
        // Alterado para bater exatamente com o método do seu Service
        Task<IEnumerable<Hardware>> BuscarHardwareComCatAsync();
    }
}