using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.EventBus.TestService.Models;
using API.EventBus.Entities;
using Microsoft.AspNetCore.SignalR;
using API.EventBus.Pusher;

namespace API.EventBus.TestService.Controllers
{
    public class HomeController : Controller
    {
        public static List<Stoc> stocList = new List<Stoc>(){
                new Stoc(){ID=1,Consumer="NetasTelekom",Value=Decimal.Parse("14.56")},
                new Stoc(){ID=2,Consumer="Bitcoin",Value=Decimal.Parse("41,583.99")},
                new Stoc(){ID=3,Consumer="Ethereum",Value=Decimal.Parse("1863.92")}
            };
        public IActionResult Index()
        {
            return View(stocList);
        }
        [HttpPost]
        public IActionResult Push(Stoc stoc)
        {
            RabbitMQPush rabbitMq = new RabbitMQPush(stoc);
            rabbitMq.Push();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Detail(int ID)
        {
            Stoc stoc = stocList.FirstOrDefault(s => s.ID == ID);
            return View(stoc);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
