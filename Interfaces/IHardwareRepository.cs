using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardReserve.Models;

namespace HardReserve.Interfaces
{
    public interface IHardwareRepository
    {
        Task<IEnumerable<Hardware>> BuscarHardwareAsync();
    }
}