using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HardReserve.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HardReserve.Controllers
{
    public class HardwareController : Controller
    {
        public IActionResult Index()
        {
            
            var listaDeHardwares = new List<Hardware>
            {
                new Hardware 
                { 
                    Id = 1, 
                    Nome = "Osciloscópio Digital", 
                    Descricao = "Equipamento para medição de sinais elétricos", 
                    QuantidadeTotal = "5", 
                    Localizacao = "Laboratório 1",
                    KitId = null
                },
                new Hardware 
                { 
                    Id = 2, 
                    Nome = "Arduino Uno", 
                    Descricao = "Placa de prototipagem", 
                    QuantidadeTotal = "20", 
                    Localizacao = "Armário A",
                    KitId = 1
                }
            };

            return View(listaDeHardwares);
        }
    }
}