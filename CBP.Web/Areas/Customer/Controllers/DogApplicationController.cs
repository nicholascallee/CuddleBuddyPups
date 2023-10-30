using CBP.DataAccess.Repository.IRepository;
using CBP.Models;
using CBP.Models.ViewModels;
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

        public DogApplicationDetailVM DogApplicationDetailVM { get; set; }

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



        public IActionResult Upsert(int? id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            DogApplicationDetail dogApplicationDetail = new()
            {
                ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId),
                ApplicationUserId = userId,
                Name = _unitOfWork.ApplicationUser.Get(u => u.Id == userId).Name
            };

            if (id.HasValue && id.Value != 0)
            {
                dogApplicationDetail = _unitOfWork.DogApplicationDetail.Get(u => u.Id == id, includeProperties: "Dog");
            }

            // Instantiate the ViewModel
            var dogApplicationDetailVM = new DogApplicationDetailVM
            {
                DogApplication = dogApplicationDetail,
                DogList = _unitOfWork.Dog.GetAll()
            };

            return View(dogApplicationDetailVM);
        }





        [HttpPost]
        public IActionResult Upsert(DogApplicationDetailVM dogApplicationDetailVM)
        {
            if (ModelState.IsValid)
            {
                if (dogApplicationDetailVM.DogApplication.Id == 0)
                {
                    _unitOfWork.DogApplicationDetail.Add(dogApplicationDetailVM.DogApplication);
                }
                else
                {
                    _unitOfWork.DogApplicationDetail.Update(dogApplicationDetailVM.DogApplication);
                }
                _unitOfWork.Save();
                TempData["success"] = "Puppy application created/updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(dogApplicationDetailVM);
            }
        }


        public IActionResult ApplicationDetails(int applicationDetailId)
        {
            CurrentDogApplication = _unitOfWork.DogApplicationDetail.Get(u => u.Id == applicationDetailId,
               includeProperties: "Dog");
            return View(CurrentDogApplication);
        }

        

        public IActionResult ApplicationConfirmation(int applicationDetailId)
        {
            CurrentDogApplication = _unitOfWork.DogApplicationDetail.Get(u => u.Id == applicationDetailId,
                          includeProperties: "Dog");
            return View(CurrentDogApplication);
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
