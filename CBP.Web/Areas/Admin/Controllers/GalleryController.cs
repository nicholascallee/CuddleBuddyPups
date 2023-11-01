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
                            ImageUrl = @"\" + imagePath + fileName
                        };


                        //add the image to the list of images in the view model
                        gallery.GalleryImages.Add(galleryImage);

                    }
                    _unitOfWork.Gallery.Update(gallery);
                    _unitOfWork.Save();
                }
                TempData["success"] = "Gallery updated successfully";
                return RedirectToAction("Gallery", "Home");
            }
            else
            {
                return RedirectToAction("Gallery","Home");
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Dog> objDogList = _unitOfWork.Dog.GetAll().ToList();
            return Json(new { data = objDogList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var dogToBeDeleted = _unitOfWork.Dog.Get(u => u.Id == id);
            if (dogToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            string dogPath = @"images\Dogs\Dog-" + id + @"\";
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, dogPath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }
                Directory.Delete(finalPath);
            }


            _unitOfWork.Dog.Remove(dogToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }


        public IActionResult DeleteImage(int imageId)
        {
            var imageToBeDeleted = _unitOfWork.DogImage.Get(u => u.Id == imageId);
            int dogId = imageToBeDeleted.DogId;
            if (imageToBeDeleted != null)
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, imageToBeDeleted.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                _unitOfWork.DogImage.Remove(imageToBeDeleted);
                _unitOfWork.Save();

                TempData["success"] = "Deleted Photo Successfully.";
            }
            return RedirectToAction(nameof(Upsert), new { id = dogId });
        }

        //public IActionResult DeleteImage(int imageId)
        //{
        //    var imageToBeDeleted = _unitOfWork.GalleryImage.Get(u => u.Id == imageId);

        //    if (imageToBeDeleted == null)
        //    {
        //        return Json(new { success = false, message = "Error while deleting" });
        //    }
        //    string imagePath = @"images\Gallery\" + imageId + @"\";
        //    string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);


        //    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, imageToBeDeleted.TrimStart('\\'));
        //    if (System.IO.File.Exists(oldImagePath))
        //    {
        //        System.IO.File.Delete(oldImagePath);
        //    }

        //    _unitOfWork.DogImage.Remove(imageToBeDeleted);
        //    _unitOfWork.Save();

        //    TempData["success"] = "Deleted Photo Successfully.";

        //    return RedirectToAction(nameof(Upsert), new { id = dogId });
        //}

        #endregion




    }
}
