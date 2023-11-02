using CBP.DataAccess.Repository.IRepository;
using CBP.Models;
using CBP.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CBP.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class GalleryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public GalleryController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            
        }

        public IActionResult Upsert()
        {
            Gallery gallery = new Gallery();

            gallery = _unitOfWork.Gallery.Get(u => u.Id == 1, includeProperties: "GalleryImages");
            return View(gallery);

        }

        [HttpPost]
        public IActionResult Upsert(Gallery gallery, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (files != null)
                {
                    foreach (IFormFile file in files)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string imagePath = @"images\Gallery\";
                        string finalPath = Path.Combine(wwwRootPath, imagePath);

                        //create folder
                        if (!Directory.Exists(finalPath))
                        {
                            Directory.CreateDirectory(finalPath);
                        }

                        //copy photos to folder
                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        //create an image
                        GalleryImage galleryImage = new()
                        {
                            ImageUrl = @"\" + imagePath + fileName,
                            GalleryId = 1
                        };


                        //add the image to the list of images in the view model
                        gallery.GalleryImages.Add(galleryImage);
                        _unitOfWork.GalleryImage.Add(galleryImage);
                        _unitOfWork.Save();


                    }
                    _unitOfWork.Gallery.Update(gallery);
                    _unitOfWork.Save();
                }
                TempData["success"] = "Gallery updated successfully";
                return RedirectToAction(nameof(Upsert));
            }
            else
            {
                return RedirectToAction(nameof(Upsert));
            }
        }

        #region API CALLS
        public IActionResult DeleteImage(int id)
        {
            var photoGallery = _unitOfWork.Gallery.Get(u => u.Id == 1, includeProperties: "GalleryImages");
            var photoToBeDeleted = photoGallery.GalleryImages.FirstOrDefault(u => u.Id == id);
            if (photoToBeDeleted == null)
            {
                TempData["success"] = "Failed To Locate and Delete Photo.";

                return RedirectToAction(nameof(Upsert));
            }
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string photoPath = photoToBeDeleted.ImageUrl;
            string finalPath = Path.Combine(wwwRootPath, photoPath);
            
            System.IO.File.Delete(wwwRootPath + photoPath);


            photoGallery.GalleryImages.Remove(photoToBeDeleted);
            _unitOfWork.Gallery.Update(photoGallery);
            _unitOfWork.Save();
            _unitOfWork.GalleryImage.Remove(photoToBeDeleted);
            _unitOfWork.Save();


            TempData["success"] = "Deleted Photo Successfully.";

            return RedirectToAction(nameof(Upsert));
        }

        #endregion
    }
}
