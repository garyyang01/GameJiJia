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
        private MongoClient _client;

        public HomeController(ILogger<HomeController> logger)
        {
            _client = new MongoClient("mongodb+srv://gary:1234qwer@test.53zvo.mongodb.net/test?retryWrites=true&w=majority");
        }

        public IActionResult Index()
        {
            var database = _client.GetDatabase("sample_mflix");
            var document = database.GetCollection<BsonDocument>("users");
            var filter = new BsonDocument("theaterId", 1000);
            var data = document.Find(x=>true).Limit(10).ToList();
            var showString = string.Join(',',data.Select(x=>x.ToString()));
            _log.Debug($"first data: {showString}");
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