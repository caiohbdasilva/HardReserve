using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardReserve.Models;

namespace HardReserve.Interfaces
{
    public interface IHardwareService
    {
        Task<IEnumerable<Hardware>> BuscarHardwareComCatAsync();

    }
}