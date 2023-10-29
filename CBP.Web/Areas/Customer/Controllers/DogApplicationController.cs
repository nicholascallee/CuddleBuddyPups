using CBP.DataAccess.Repository.IRepository;
using CBP.Models;
using CBP.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CBP.Web.Areas.Customer.Controllers
{
    [Area("customer")]
    [Authorize]
    public class DogApplicationController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public IEnumerable <DogApplicationDetail> DogApplicationList { get; set; }
        public DogApplicationDetail CurrentDogApplication { get; set; }
        public DogApplicationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            DogApplicationList = _unitOfWork.DogApplicationDetail.GetAll(u => u.ApplicationUserId == userId,
                includeProperties: "Dog");

            IEnumerable<DogImage> dogImages = _unitOfWork.DogImage.GetAll();

            return View(DogApplicationList);
        }



        public IActionResult ApplicationDetails(int applicationDetailId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            CurrentDogApplication = _unitOfWork.DogApplicationDetail.Get(u => u.Id == applicationDetailId,
                includeProperties: "Dog");
            return View(CurrentDogApplication);
        }

        [HttpPost]
        [ActionName("ApplicationDetails")]
        public IActionResult ApplicationDetailsPOST()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            DogApplicationList = _unitOfWork.DogApplicationDetail.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Dog");

            CurrentDogApplication.ApplicationDate = DateTime.Now;
            CurrentDogApplication.ApplicationUserId = userId;



            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);


            _unitOfWork.DogApplicationDetail.Add(CurrentDogApplication);
            _unitOfWork.Save();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult ApplicationConfirmation(int id)
        {
            return View(id);
        }
        public IActionResult Remove(int dogApplicationId)
        {
            var DogApplicationFromDb = _unitOfWork.DogApplicationDetail.Get(u => u.Id == dogApplicationId, tracked: true);

            HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.DogApplicationDetail.GetAll(u => u.ApplicationUserId == DogApplicationFromDb.ApplicationUserId).Count() - 1);
            _unitOfWork.DogApplicationDetail.Remove(DogApplicationFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }





    }
}
