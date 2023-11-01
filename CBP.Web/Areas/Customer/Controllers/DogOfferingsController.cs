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
    public class DogOfferingsController : Controller
    {



        private readonly ILogger<DogOfferingsController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public DogOfferingsController(ILogger<DogOfferingsController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            IEnumerable<Dog> dogList = _unitOfWork.Dog.GetAll(includeProperties: "DogImages");
            return View(dogList);
        }

        public IActionResult Details(int dogId)
        {
            var dog = _unitOfWork.Dog.Get(u => u.Id == dogId, includeProperties: "DogImages");
                
            return View(dog);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
        
}
