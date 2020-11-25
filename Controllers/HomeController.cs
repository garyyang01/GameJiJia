using GameJiJia.Models;
using GameJiJia.Service;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GameJiJia.Controllers
{
    public class HomeController : Controller
    {
        private ILog _log = LogManager.GetLogger(Startup.repository.Name, typeof(HomeController));
        private GameJiJiaService _gameJiJiaService;

        public HomeController()
        {
            _gameJiJiaService = new GameJiJiaService();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string accountId, string password)
        {
            _log.Info($"accoumtId : {accountId}, password : {password}");
            var response= await _gameJiJiaService.LoginUser(new UserInfo()
            {
                AccountId = accountId,
                Password = password
            });
            if (response)
            {
                return RedirectToAction("Home");
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string accountId, string password, string emailAddress)
        {
            _log.Info($"accoumtId : {accountId}, password : {password}, emailAddress: {emailAddress}");
            var response = await _gameJiJiaService.RegisterUser(new UserInfo()
            {
                AccountId = accountId,
                Password = password,
                EmailAddress = emailAddress
            });
            if (response)
            {
                return RedirectToAction("Home");
            }
            return RedirectToAction("Error");
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