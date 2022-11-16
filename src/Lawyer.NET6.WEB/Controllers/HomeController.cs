using Lawyer.NET6.BLL.Service;
using Lawyer.NET6.BLL.ViewModel;
using Lawyer.NET6.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lawyer.NET6.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LawyerService _lawyerService;
        public HomeController(ILogger<HomeController> logger, LawyerService lawyerService)
        {
            _logger = logger;
            _lawyerService = lawyerService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Service()
        {
            return View();
        }
        public IActionResult Connect()
        {
            return View();
        }
        public IActionResult FreeLegalAssistance()
        {
             return View();
        }
        public IActionResult Reconciliation()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Json

        [HttpPost]
        public async Task<JsonResult> SendEmail([FromBody] EmailViewModel model)
        {
            try
            {
                var res = await _lawyerService.Check_and_Send_Feedback(model);
                return Json(res);
            }
            catch(Exception ex)
            {
                return Json(new {Error = true, ErrorMessage = ex.Message });
            }
        }

        #endregion
    }
}