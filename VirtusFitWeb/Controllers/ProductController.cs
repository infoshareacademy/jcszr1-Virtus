using BLL;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VirtusFitWeb.Services;

namespace VirtusFitWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFavoriteService _favoriteService;

        public ProductController(IProductService productService, IFavoriteService favoriteService)
        {
            _productService = productService;
            _favoriteService = favoriteService;
        }

        public IActionResult ProductList()
        {
            return View(_productService.GetAll().ToList());
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = _productService.GetById(id);
            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(int id, Product productToBeDeleted)
        {
            try
            {
                _productService.DeleteById(id);
                _favoriteService.DeleteFromFavorites(_favoriteService.GetById(id));
                return RedirectToAction(nameof(ProductList));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetById(id);
            if (product != null)
            {
                return View(product);
            }
            else
            {
                return RedirectToAction(nameof(ProductList));
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            try
            {
                _productService.Update(id, product);
                return RedirectToAction(nameof(ProductList));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product newProduct)
        {
            if (!ModelState.IsValid)
            {
                return View(newProduct);
            }
            try
            {
                newProduct = _productService.Create(newProduct);
                return RedirectToAction("Details", "Product", new { id = newProduct.ProductId });
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult AddToFavorites(int id)
        {
            var favorite = _productService.GetById(id);
            _productService.AddToFavorites(favorite);
            return RedirectToAction(nameof(ProductList));
        }

        [HttpGet]
        public IActionResult DeleteFromFavorites(int id)
        {
            var favorite = _productService.GetById(id);
            _productService.DeleteFromFavorites(favorite);
            return RedirectToAction(nameof(ProductList));
        }
    }
}
