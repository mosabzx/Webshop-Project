using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            List<Item> cart = new List<Item>();
            cart.Clear();
            return View();
        }
    }
}
