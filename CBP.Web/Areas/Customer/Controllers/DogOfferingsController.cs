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
            ShoppingCart cart = new()
            {
                Dog = _unitOfWork.Dog.Get(u => u.Id == dogId, includeProperties: "DogImages"),
                Count = 1,
                DogId = dogId
            };
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId && u.DogId == shoppingCart.DogId);


            if (cartFromDb != null)
            {
                cartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
                _unitOfWork.Save();
            }
            else
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
            }
            TempData["success"] = "Cart Updated Successfully";



            return RedirectToAction(nameof(Index));
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
    }
        
}
