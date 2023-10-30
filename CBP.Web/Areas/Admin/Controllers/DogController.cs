using CBP.DataAccess.Repository.IRepository;
using CBP.Models;
using CBP.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CBP.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class DogController : Controller
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DogController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            
        }

        public IActionResult Index()
        {
            List<Dog> objDogList = _unitOfWork.Dog.GetAll(includeProperties: "DogImages").ToList();
            return View(objDogList);
        }

        public IActionResult Upsert(int? id)
        {
            Dog dog = new Dog();

            if (id == null || id == 0)
            {
                return View(dog);
            }
            else
            {
                dog = _unitOfWork.Dog.Get(u => u.Id == id, includeProperties: "DogImages");
                return View(dog);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Dog dog, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                if (dog.Id == 0)
                {
                    _unitOfWork.Dog.Add(dog);
                }
                else
                {
                    _unitOfWork.Dog.Update(dog);
                }


                _unitOfWork.Save();

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (files != null)
                {
                    //if the current dog does not contain a list of dog images then make one
                    if (dog.DogImages == null)
                    {
                        dog.DogImages = new List<DogImage>();
                    }

                    foreach (IFormFile file in files)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string dogPath = @"images\Dogs\Dog-" + dog.Id + @"\";
                        string finalPath = Path.Combine(wwwRootPath, dogPath);

                        //create folder for dog
                        if (!Directory.Exists(finalPath))
                        {
                            Directory.CreateDirectory(finalPath);
                        }
                        //copy photos to folder for dog
                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        //create a dog image
                        DogImage dogImage = new()
                        {
                            ImageUrl = @"\" + dogPath + fileName,
                            DogId = dog.Id,
                        };


                        //add the dog image to the list of images in the view model
                        dog.DogImages.Add(dogImage);

                    }
                    _unitOfWork.Dog.Update(dog);
                    _unitOfWork.Save();
                }


                TempData["success"] = "Dog created/updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(dog);
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

        #endregion




    }
}
