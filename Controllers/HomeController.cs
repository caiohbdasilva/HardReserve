using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HardReserve.Models;

namespace HardReserve.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }



}
