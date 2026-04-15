using Bulky.Utility;
using BulkyDataAccess;
using BulkyDataAccess.Repositry.IRepositry;
using BulkyModels.Models;
using BulkyModels.ViewModels;
using BulkyWebModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment) {
            _unitOfWork = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> objCategoryList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            return View(objCategoryList);
        }
        public IActionResult Upsert(int? Id)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = CategoryList
            };
            if (Id == null || Id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == Id);
                return View(productVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    if (!string.IsNullOrEmpty(obj.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.Product.ImageUrl = @"\images\product\" + fileName;
                }
                if(obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                }
                _unitOfWork.Product.Add(obj.Product);
                TempData["success"] = "Category is created successfully";
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                ProductVM productVM = new ProductVM()
                {
                    Product = new Product(),
                    CategoryList = CategoryList
                };
                return View(productVM);
            }
        }
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Product? catergoryFromDb = _unitOfWork.Product.Get(u=>u.Id==id);
            if (catergoryFromDb == null)
            {
                return NotFound();
                }
            return View(catergoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                TempData["success"] = "Category is updated successfully";
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? catergoryFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            //Catergory? catergoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Catergory? catergoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (catergoryFromDb == null)
            {
                return NotFound();
            }
            return View(catergoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            Product obj= _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null) { 
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category is deleted successfully";
            return RedirectToAction("Index");
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() {
            List<Product> objCategoryList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objCategoryList });

        }
        #endregion
    }
}
