using Microsoft.AspNetCore.Mvc;
using HardReserve.Interfaces;
using HardReserve.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HardReserve.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IReservaService _reservaService;

        public ReservaController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        private bool EstaLogado() => HttpContext.Session.GetString("UsuarioId") != null;

        public async Task<IActionResult> Listagem()
        {
            if (!EstaLogado())
            {
                return RedirectToAction("Index", "Login");
            }

            var listaReservas = await _reservaService.ListarReservasAsync();
            return View(listaReservas);
        }

        [HttpGet]
        public async Task<IActionResult> Criar(int? hardwareId)
        {
            if (!EstaLogado())
            {
                TempData["Erro"] = "Você precisa estar logado para fazer uma reserva.";
                return RedirectToAction("Index", "Login");
            }

            var disponiveis = await _reservaService.ListarHardwaresDisponiveisAsync();
            ViewBag.HardwarePreSelecionado = hardwareId;

            return View(disponiveis);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(DateTime DataInicial, DateTime DataFinal,
                                               List<int> HardwareIds, List<int> Quantidades)
        {
            if (!EstaLogado())
            {
                TempData["Erro"] = "Você precisa estar logado para fazer uma reserva.";
                return RedirectToAction("Index", "Login");
            }

            if (HardwareIds == null || !HardwareIds.Any())
            {
                TempData["Erro"] = "Adicione pelo menos um hardware ao carrinho antes de confirmar.";
                return RedirectToAction("Criar");
            }

            if (DataFinal <= DataInicial)
            {
                TempData["Erro"] = "A data de devolução deve ser posterior à data de retirada.";
                return RedirectToAction("Criar");
            }

            var itens = new Dictionary<int, int>();
            for (int i = 0; i < HardwareIds.Count; i++)
            {
                var id = HardwareIds[i];

                var qtd = (Quantidades != null && i < Quantidades.Count) ? Quantidades[i] : 1;

                if (itens.ContainsKey(id))
                    itens[id] += qtd;
                else
                    itens[id] = qtd;
            }

            var usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId")!);

            var reserva = new Reserva
            {
                UsuarioId = usuarioId,
                DataInicial = DataInicial,
                DataFinal = DataFinal
            };

            var (ok, erro, reservaId) = await _reservaService.CriarReservaAsync(reserva, itens);

            if (!ok)
            {
                TempData["Erro"] = erro;
                return RedirectToAction("Criar");
            }

            return RedirectToAction("Comprovante", new { id = reservaId });
        }

        public async Task<IActionResult> Comprovante(int id)
        {
            if (!EstaLogado())
            {
                return RedirectToAction("Index", "Login");
            }

            var reserva = await _reservaService.BuscarReservaPorIdAsync(id);
            if (reserva == null)
            {
                TempData["Erro"] = "Reserva não encontrada.";
                return RedirectToAction("Listagem");
            }

            ViewBag.Hardwares = await _reservaService.BuscarHardwaresDaReservaAsync(id);

            return View(reserva);
        }

        [HttpPost]
        public async Task<IActionResult> MudarStatus(int id, string novoStatus)
        {
            if (!EstaLogado())
            {
                return RedirectToAction("Index", "Login");
            }

            if (HttpContext.Session.GetString("UsuarioRole") != "T")
            {
                TempData["Erro"] = "Apenas o técnico pode alterar o status das reservas.";
                return RedirectToAction("Listagem");
            }

            var statusValidos = new[] { "PE", "AP", "CA", "RE", "DE", "AT" };
            if (!statusValidos.Contains(novoStatus))
            {
                TempData["Erro"] = "Status inválido.";
                return RedirectToAction("Listagem");
            }

            await _reservaService.AtualizarStatusAsync(id, novoStatus);

            TempData["Sucesso"] = "Status da reserva atualizado com sucesso!";
            return RedirectToAction("Listagem");
        }
    }
}
