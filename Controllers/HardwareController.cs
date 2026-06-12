using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HardReserve.Interfaces;
using HardReserve.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HardReserve.Controllers
{
    public class HardwareController : Controller
    {
        private readonly IHardwareService _hardwareService;

        public HardwareController(IHardwareService hardwareService)
        {
            _hardwareService = hardwareService;
        }
        public async Task<IActionResult> Index()
        {

            var Hardware = await _hardwareService.BuscarHardwareComCatAsync();
            return View(Hardware);
        }
    }
}