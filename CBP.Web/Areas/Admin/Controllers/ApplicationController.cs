using CBP.DataAccess.Repository.IRepository;
using CBP.Models;
using CBP.Models.ViewModels;
using CBP.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CBP.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class ApplicationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ApplicationVM ApplicationVM { get; set; }

        public ApplicationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int applicationId)
        {
            ApplicationVM = new()
            {
                ApplicationHeader = _unitOfWork.ApplicationHeader.Get(u => u.Id == applicationId, includeProperties: "ApplicationUser"),
                ApplicationDetail = _unitOfWork.ApplicationDetail.Get(u => u.ApplicationHeaderId == applicationId, includeProperties: "Dog")
            };

            return View(ApplicationVM);
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult UpdateApplicationDetail(int applicationId)
        {
            var ApplicationHeaderFromDb = _unitOfWork.ApplicationHeader.Get(u => u.Id == ApplicationVM.ApplicationHeader.Id);
            ApplicationHeaderFromDb.Name = ApplicationVM.ApplicationHeader.Name;
            ApplicationHeaderFromDb.PhoneNumber = ApplicationVM.ApplicationHeader.PhoneNumber;
            ApplicationHeaderFromDb.StreetAddress = ApplicationVM.ApplicationHeader.StreetAddress;
            ApplicationHeaderFromDb.City = ApplicationVM.ApplicationHeader.City;
            ApplicationHeaderFromDb.State = ApplicationVM.ApplicationHeader.State;
            ApplicationHeaderFromDb.PostalCode = ApplicationVM.ApplicationHeader.PostalCode;
            if (!string.IsNullOrEmpty(ApplicationVM.ApplicationHeader.Answer1))
            {
                ApplicationHeaderFromDb.Answer1 = ApplicationVM.ApplicationHeader.Answer1;
            }
             if (!string.IsNullOrEmpty(ApplicationVM.ApplicationHeader.Answer2))
            {
                ApplicationHeaderFromDb.Answer2 = ApplicationVM.ApplicationHeader.Answer2;
            }
             if (!string.IsNullOrEmpty(ApplicationVM.ApplicationHeader.Answer3))
            {
                ApplicationHeaderFromDb.Answer3 = ApplicationVM.ApplicationHeader.Answer3;
            }
             if (!string.IsNullOrEmpty(ApplicationVM.ApplicationHeader.Answer4))
            {
                ApplicationHeaderFromDb.Answer4 = ApplicationVM.ApplicationHeader.Answer4;
            }
             if (!string.IsNullOrEmpty(ApplicationVM.ApplicationHeader.Answer5))
            {
                ApplicationHeaderFromDb.Answer5 = ApplicationVM.ApplicationHeader.Answer5;
            }
            _unitOfWork.ApplicationHeader.Update(ApplicationHeaderFromDb);
            _unitOfWork.Save();
            TempData["Success"] = "Application Details Updated Successfully.";
            return RedirectToAction(nameof(Details), new { ApplicationId = ApplicationHeaderFromDb.Id });
         }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult SetPaymentStatus(string paymentStatus)
        {
            _unitOfWork.ApplicationHeader.UpdateStatus(ApplicationVM.ApplicationHeader.Id, ApplicationVM.ApplicationHeader.ApplicationStatus,paymentStatus);
            _unitOfWork.Save();
            TempData["Success"] = "Application status Updated Successfully.";
            return RedirectToAction(nameof(Details), new { ApplicationId = ApplicationVM.ApplicationHeader.Id });
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult SetApplicationStatus(string applicationStatus)
        {
            var ApplicationHeader = _unitOfWork.ApplicationHeader.Get(u => u.Id == ApplicationVM.ApplicationHeader.Id);
            if (ApplicationHeader.PaymentStatus == SD.StatusApproved)
            {
               _unitOfWork.ApplicationHeader.UpdateStatus(ApplicationHeader.Id, applicationStatus);
               _unitOfWork.Save();
            }
            TempData["Success"] = "Application Updated Successfully.";
            return RedirectToAction(nameof(Details), new { ApplicationId = ApplicationVM.ApplicationHeader.Id });
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<ApplicationHeader> objApplicationHeaders = _unitOfWork.ApplicationHeader.GetAll(includeProperties: "ApplicationUser").ToList();

            if(User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
            {
                objApplicationHeaders = _unitOfWork.ApplicationHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                objApplicationHeaders = _unitOfWork.ApplicationHeader.GetAll(u => u.ApplicationUserId == userId, includeProperties: "ApplicationUser");
            }

            switch (status)
            {
                case "in review":
                    objApplicationHeaders = objApplicationHeaders.Where(u => u.PaymentStatus == SD.StatusInReview);
                    break;
                case "approved":
                    objApplicationHeaders = objApplicationHeaders.Where(u => u.ApplicationStatus == SD.StatusApproved);
                    break;
                case "completed":
                    objApplicationHeaders = objApplicationHeaders.Where(u => u.ApplicationStatus == SD.StatusFinalized); 
                    break;
                case "cancelled":
                    objApplicationHeaders = objApplicationHeaders.Where(u => u.ApplicationStatus == SD.StatusCancelled); 
                    break;
                default:
                    break;
            }




            return Json(new { data = objApplicationHeaders });
        }

        #endregion


    }
}
