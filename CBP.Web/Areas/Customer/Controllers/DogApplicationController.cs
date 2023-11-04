//using CBP.DataAccess.Repository.IRepository;
//using CBP.Models;
//using CBP.Models.ViewModels;
//using CBP.Utility;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace CBP.Web.Areas.Customer.Controllers
//{
//    [Area("customer")]
//    [Authorize]
//    public class DogApplicationController : Controller
//    {

//        private readonly IUnitOfWork _unitOfWork;
//        [BindProperty]
//        public IEnumerable <ApplicationDetail> DogApplicationList { get; set; }
//        public ApplicationDetail CurrentDogApplication { get; set; }
//        public Dog currentDog { get; set; }

//        public ApplicationVM DogApplicationDetailVM { get; set; }

//        public DogApplicationController(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }


//        public IActionResult Index()
//        {
//            var claimsIdentity = (ClaimsIdentity)User.Identity;
//            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
//            DogApplicationList = _unitOfWork.DogApplicationDetail.GetAll(u => u.ApplicationUserId == userId,
//                includeProperties: "Dog");

//            IEnumerable<DogImage> dogImages = _unitOfWork.DogImage.GetAll();

//            return View(DogApplicationList);
//        }



//        public IActionResult Upsert(int? id, int? dogId)
//        {
//            var claimsIdentity = (ClaimsIdentity)User.Identity;
//            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
//            var appUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

//            ApplicationDetail dogApplicationDetail;
//            if (id.HasValue && id.Value != 0)
//            {
//                dogApplicationDetail = _unitOfWork.DogApplicationDetail.Get(u => u.Id == id, includeProperties: "Dog");
//            }
//            if(dogId.HasValue && dogId.Value != 0)
//            {
//                dogApplicationDetail = new()
//                {
//                    ApplicationUser = appUser,
//                    ApplicationUserId = userId,
//                    City = appUser.City,
//                    State = appUser.State,
//                    PostalCode = appUser.PostalCode,
//                    StreetAddress = appUser.StreetAddress,
//                    PhoneNumber = appUser.PhoneNumber,
//                    Name = appUser.Name,
//                    DogId = dogId.Value,
//                    Dog = _unitOfWork.Dog.Get(u => u.Id == dogId)

//                };


//                currentDog = _unitOfWork.Dog.Get(u => u.Id == dogId);
//                // Instantiate the ViewModel with the given dog
//                var dogApplicationDetailVM = new ApplicationVM
//                {
//                    DogApplication = dogApplicationDetail,
//                    CurrentDog = currentDog,
//                    DogList = _unitOfWork.Dog.GetAll()
//                };
//                return View(dogApplicationDetailVM);

//            }
//            else
//            {
//                dogApplicationDetail = new()
//                {
//                    ApplicationUser = appUser,
//                    ApplicationUserId = userId,
//                    City = appUser.City,
//                    State = appUser.State,
//                    PostalCode = appUser.PostalCode,
//                    StreetAddress = appUser.StreetAddress,
//                    PhoneNumber = appUser.PhoneNumber,
//                    Name = appUser.Name,

//                };
//                // Instantiate the ViewModel with no dog
//                var dogApplicationDetailVM = new ApplicationVM
//                {
//                    DogApplication = dogApplicationDetail,
//                    DogList = _unitOfWork.Dog.GetAll()
//                };
//                return View(dogApplicationDetailVM);
//            }
//        }




/////here is where we start today
//        [HttpPost]
//        public IActionResult Upsert(ApplicationVM dogApplicationDetailVM)
//        {
//            if (ModelState.IsValid)
//            {
//                if (dogApplicationDetailVM.DogApplication.Id == 0)
//                {
//                    _unitOfWork.DogApplicationDetail.Add(dogApplicationDetailVM.DogApplication);
//                }
//                else
//                {
//                    _unitOfWork.DogApplicationDetail.Update(dogApplicationDetailVM.DogApplication);
//                }
//                _unitOfWork.Save();
//                TempData["success"] = "Puppy application created/updated successfully";
//                return RedirectToAction("Index");
//            }
//            else
//            {
//                return View(dogApplicationDetailVM);
//            }
//        }


//        public IActionResult ApplicationDetails(int applicationDetailId)
//        {
//            CurrentDogApplication = _unitOfWork.DogApplicationDetail.Get(u => u.Id == applicationDetailId,
//               includeProperties: "Dog");
//            return View(CurrentDogApplication);
//        }

        

//        public IActionResult ApplicationConfirmation(int applicationDetailId)
//        {
//            CurrentDogApplication = _unitOfWork.DogApplicationDetail.Get(u => u.Id == applicationDetailId,
//                          includeProperties: "Dog");
//            return View(CurrentDogApplication);
//        }

//        public IActionResult Remove(int dogApplicationId)
//        {
//            var DogApplicationFromDb = _unitOfWork.DogApplicationDetail.Get(u => u.Id == dogApplicationId, tracked: true);

//            HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.DogApplicationDetail.GetAll(u => u.ApplicationUserId == DogApplicationFromDb.ApplicationUserId).Count() - 1);
//            _unitOfWork.DogApplicationDetail.Remove(DogApplicationFromDb);
//            _unitOfWork.Save();
//            return RedirectToAction(nameof(Index));
//        }





//    }
//}
