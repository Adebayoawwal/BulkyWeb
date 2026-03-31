using BulkyDataAccess;
using BulkyDataAccess.Repositry.IRepositry;
using BulkyWebModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork db) {
            _unitOfWork = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly watch the Name.");
            }
            if(obj.Name!=null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invaild value"); 
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                TempData["success"] = "Category is created successfully";
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category? catergoryFromDb = _unitOfWork.Category.Get(u=>u.Id==id);
            if (catergoryFromDb == null)
            {
                return NotFound();
            }
            return View(catergoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly watch the Name.");
            }
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invaild value");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
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
            Category? catergoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
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
            Category obj= _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null) { 
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category is deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
