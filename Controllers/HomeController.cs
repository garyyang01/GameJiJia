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
            var loginSuccess= await _gameJiJiaService.LoginUser(new UserInfo()
            {
                AccountId = accountId,
                Password = password
            });
            if (loginSuccess)
            {
                return RedirectToAction("Home");
            }
            TempData["status"] = "Wrong username or password. Please try again.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string accountId, string password, string emailAddress)
        {
            _log.Info($"accoumtId : {accountId}, password : {password}, emailAddress: {emailAddress}");
            var registerSuccess = await _gameJiJiaService.RegisterUser(new UserInfo()
            {
                AccountId = accountId,
                Password = password,
                EmailAddress = emailAddress
            });
            if (registerSuccess)
            {
                return RedirectToAction("Home");
            }

            TempData["status"] = "This username has been used. Please change another username.";
            return RedirectToAction("Index");
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