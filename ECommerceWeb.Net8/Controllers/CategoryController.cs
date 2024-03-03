using ECommerceWeb.Net8.Data;
using ECommerceWeb.Net8.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.Net8.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {         
            return View();
        }

        [HttpPost]
        public IActionResult Create( Category obj)
        {
            if ( obj.Name==obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Category Name and Display order should not be same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Sucessfully";
                return RedirectToAction("Index");
            }
            return View();
           
        }

        public IActionResult Edit(int? Id)
        {
            if(Id==null || Id==0)
            {
                return NotFound();
            }
            Category? categoryFromDb= _db.Categories.Find(Id);
            //Category? category1 = _db.Categories.FirstOrDefault(u => u.C_Id==Id);
            //Category? category2 = _db.Categories.Where(u => u.C_Id==Id).FirstOrDefault();

            if (categoryFromDb == null )
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid )
            {
           
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated Sucessfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(Id);
            //Category? category1 = _db.Categories.FirstOrDefault(u => u.C_Id==Id);
            //Category? category2 = _db.Categories.Where(u => u.C_Id==Id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
  
                Category? removeCategory= _db.Categories.Find(Id);
                if(removeCategory == null )
                {
                    return NotFound();
                }
                _db.Categories.Remove(removeCategory);
                _db.SaveChanges();
            TempData["success"] = "Category deleted Sucessfully";
            return RedirectToAction("Index");
      
        }

    }
}
