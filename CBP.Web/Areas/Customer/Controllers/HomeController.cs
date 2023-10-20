using CBP.DataAccess.Repository.IRepository;
using CBP.Models.ViewModels;
using CBP.Models;
using CBP.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CBP.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            return View();
        }
        public IActionResult GoldenDoodleInfo()
        {

            return View();
        }
        public IActionResult Gallery()
        {

            return View();
        }
        public IActionResult ContractAndPolicies()
        {

            return View();
        }
        
        public IActionResult About()
        {

            return View();
        }
        public IActionResult Bootcamp()
        {

            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}