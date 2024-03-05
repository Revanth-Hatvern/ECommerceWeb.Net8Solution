using EcommerceWeb.Models;
using EcommerceWebDataAccess.Repository;
using EcommerceWebDataAccess.Repository.IRepository;
using ECommerceWebDataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.Net8.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.category.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Category Name and Display order should not be same");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Sucessfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            //   Category? categoryFromDb= _db.Categories.Find(Id);
            Category? categoryFromDb = _unitOfWork.category.Get(u => u.Id == Id);

            //Category? category1 = _db.Categories.FirstOrDefault(u => u.C_Id==Id);
            //Category? category2 = _db.Categories.Where(u => u.C_Id==Id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {

                _unitOfWork.category.Update(obj);
                _unitOfWork.Save();
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
            Category? categoryFromDb = _unitOfWork.category.Get(u => u.Id == Id);
            //Category? category1 = _db.Categories.FirstOrDefault(u => u.C_Id==Id);
            //Category? category2 = _db.Categories.Where(u => u.C_Id==Id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {

            Category? removeCategory = _unitOfWork.category.Get(u => u.Id == Id);
            if (removeCategory == null)
            {
                return NotFound();
            }
            _unitOfWork.category.Remove(removeCategory);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted Sucessfully";
            return RedirectToAction("Index");

        }

    }
}
