
using EcommerceWeb.Models;
using EcommerceWebDataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ECommerceWeb.Net8.Areas.Customer.Controllers
{

    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitofWork)
        {
            _logger = logger;
            _unitOfWork = unitofWork;
        }

        public IActionResult Index()
        {
            List<Product> objProductsList = _unitOfWork.product.GetAll(includeProperties:"Category").ToList();
            return View(objProductsList);
        }

        public IActionResult Details(int productId)
        {
            Product product = _unitOfWork.product.Get(u => u.Id == productId, includeProperties: "Category");
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
