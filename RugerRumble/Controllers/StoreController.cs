using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RugerRumble.Models;

namespace RugerRumble.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
           return View();
        }
    }
}
