using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HardReserve.Models;
using Microsoft.AspNetCore.Http;

namespace HardReserve.Interfaces
{
    public interface IHardwareService
    {
        Task<IEnumerable<Hardware>> BuscarHardwareComCatAsync();

        Task CadastrarHardwareAsync(Hardware hardware, IFormFile? arquivoImagem);

        Task<Hardware?> BuscarHardwarePorIdAsync(int id);

        Task AtualizarHardwareAsync(Hardware hardware, IFormFile? arquivoImagem);

        Task<bool> ExcluirHardwareAsync(int id);
    }
}
