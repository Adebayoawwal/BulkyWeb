using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Catergory> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Catergory obj)
        {
            if(obj.Name == obj.DiplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly watch the Name.");
            }
            if(obj.Name!=null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invaild value"); 
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index"  );
        }
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Catergory? catergoryFromDb = _db.Categories.Find(id);
            //Catergory? catergoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Catergory? catergoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (catergoryFromDb == null)
            {
                return NotFound();
            }
            return View(catergoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Catergory obj)
        {
            if (obj.Name == obj.DiplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly watch the Name.");
            }
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invaild value");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Catergory? catergoryFromDb = _db.Categories.Find(id);
            //Catergory? catergoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Catergory? catergoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (catergoryFromDb == null)
            {
                return NotFound();
            }
            return View(catergoryFromDb);
        }
        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
