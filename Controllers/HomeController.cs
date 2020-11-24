using GameJiJia.Models;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Diagnostics;
using System.Linq;
using MongoDB.Bson;

namespace GameJiJia.Controllers
{
    public class HomeController : Controller
    {
        private ILog _log = LogManager.GetLogger(Startup.repository.Name, typeof(HomeController));

        public HomeController(ILogger<HomeController> logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            _log.Info("Privacy Controller");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _log.Error("error Controller");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}