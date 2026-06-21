using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HardReserve.Interfaces;
using HardReserve.Models;
using Microsoft.AspNetCore.Http;

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

        public async Task CadastrarHardwareAsync(Hardware hardware, IFormFile? arquivoImagem)
        {
            if (arquivoImagem != null && arquivoImagem.Length > 0)
            {
                hardware.Imagem = await UploadImagemAsync(arquivoImagem);
            }
            else
            {
                hardware.Imagem = "";
            }

            await _hardwareRepository.CadastrarHardwareAsync(hardware);
        }

        public async Task<Hardware?> BuscarHardwarePorIdAsync(int id)
        {
            return await _hardwareRepository.BuscarHardwarePorIdAsync(id);
        }

        public async Task AtualizarHardwareAsync(Hardware hardware, IFormFile? arquivoImagem)
        {
            if (arquivoImagem != null && arquivoImagem.Length > 0)
            {
                hardware.Imagem = await UploadImagemAsync(arquivoImagem);
            }

            await _hardwareRepository.AtualizarHardwareAsync(hardware);
        }

        public async Task<bool> ExcluirHardwareAsync(int id)
        {
            return await _hardwareRepository.ExcluirHardwareAsync(id);
        }

        private async Task<string> UploadImagemAsync(IFormFile arquivoImagem)
        {
            string caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "hardwares");

            if (!Directory.Exists(caminhoPasta))
            {
                Directory.CreateDirectory(caminhoPasta);
            }

            var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(arquivoImagem.FileName);

            var caminhoArquivo = Path.Combine(caminhoPasta, nomeArquivo);

            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                await arquivoImagem.CopyToAsync(stream);
            }

            return $"img/hardwares/{nomeArquivo}";
        }
    }
}
