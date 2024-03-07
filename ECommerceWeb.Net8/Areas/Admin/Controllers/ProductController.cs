using EcommerceWeb.Models;
using EcommerceWeb.Models.ViewModels;
using EcommerceWebDataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceWeb.Net8.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.product.GetAll().ToList();
            return View(objProductList);
        }
        public IActionResult Upsert(int? Id)      //upsert // Update and Insert
        {  
       
            ProductVM productVM = new()                          //SelectListItem === ProjectionCencept      //  IEnumerable<SelectListItem> CategoryList = 
            {
                CategoryList = _unitOfWork.category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()

                }),
            Product =new Product()
            };

            if(Id==0 || Id==null)
            {
                //Create
                return View(productVM);
            }
            else
            {
                //Update
                productVM.Product = _unitOfWork.product.Get(u => u.Id == Id);
                return View(productVM);
            }
                     
            // ViewBag.CategoryList = CategoryList;
            // ViewData["CategoryList"] = CategoryList;
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product Created Sucessfully";
                return RedirectToAction("Index");
   
            }
            else
            {              
                    productVM.CategoryList = _unitOfWork.category
               .GetAll().Select(u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.Id.ToString()

               });

                return View(productVM);
            }
            

        }

     
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.product.Get(u => u.Id == Id);
            //Product? product1 = _db.Categories.FirstOrDefault(u => u.C_Id==Id);
            //Product? product2 = _db.Categories.Where(u => u.C_Id==Id).FirstOrDefault();

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {

            Product? removeProduct = _unitOfWork.product.Get(u => u.Id == Id);
            if (removeProduct == null)
            {
                return NotFound();
            }
            _unitOfWork.product.Remove(removeProduct);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted Sucessfully";
            return RedirectToAction("Index");

        }
    }
}
