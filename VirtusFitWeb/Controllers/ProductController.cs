using BLL;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VirtusFitWeb.Services;

namespace VirtusFitWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFavoriteService _favoriteService;

        public string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
        public string Username => User.FindFirstValue(ClaimTypes.Name);

        public ProductController(IProductService productService, IFavoriteService favoriteService)
        {
            _productService = productService;
            _favoriteService = favoriteService;
        }

        public IActionResult ProductList()
        {
            return View(_productService.GetAll(UserId));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = _productService.GetById(id);
            return View(product);
        }

        [HttpGet]
        public IActionResult DeleteConfirm(int id)
        {
            var product = _productService.GetById(id);
            return View("Delete", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_productService.GetById(id).IsFavorite)
                {
                    _favoriteService.DeleteFromFavorites(_favoriteService.GetById(id), UserId, Username);
                }

                _productService.DeleteById(id, UserId, Username);
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
                _productService.Update(id, product, UserId, Username);
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
                newProduct.UserId = UserId;
                newProduct = _productService.Create(newProduct, UserId, Username);
                return RedirectToAction("Details", "Product", new { id = newProduct.ProductId });
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchByName()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchByFat()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchByCalories()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchByCarbohydrates()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchByProteins()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchByNameResults(SearchCriteria criteria)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                return View(_productService.SearchByName(criteria.ProductName, UserId, Username));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult SearchByFatResults(SearchCriteria criteria)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                double minfat = criteria.MinFat.HasValue ? criteria.MinFat.Value : 0;
                double maxfat = criteria.MaxFat.HasValue ? criteria.MaxFat.Value : int.MaxValue;

                return View(_productService.SearchByFat(minfat, maxfat, UserId, Username));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult SearchByCaloriesResults(SearchCriteria criteria)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                double mincal = criteria.MinEnergy.HasValue ? criteria.MinEnergy.Value : 0;
                double maxcal = criteria.MaxEnergy.HasValue ? criteria.MaxEnergy.Value : int.MaxValue;

                return View(_productService.SearchByCalories(mincal, maxcal, UserId, Username));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult SearchByCarbohydratesResults(SearchCriteria criteria)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                double mincarb = criteria.MinCarbohydrates.HasValue ? criteria.MinCarbohydrates.Value : 0;
                double maxcarb = criteria.MaxCarbohydrates.HasValue ? criteria.MaxCarbohydrates.Value : int.MaxValue;

                return View(_productService.SearchByCarbohydrates(mincarb, maxcarb, UserId, Username));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult SearchByProteinsResults(SearchCriteria criteria)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                double minprotein = criteria.MinProtein.HasValue ? criteria.MinProtein.Value : 0;
                double maxprotein = criteria.MaxProtein.HasValue ? criteria.MaxProtein.Value : int.MaxValue;

                return View(_productService.SearchByProteins(minprotein, maxprotein, UserId, Username));
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public IActionResult AddToFavorites(int id)
        {
            var userId = UserId;
            var username = Username;
            var favorite = _productService.GetById(id);
            _productService.AddToFavorites(favorite, userId, username);
            return RedirectToAction(nameof(ProductList));
        }

        [HttpGet]
        public IActionResult DeleteFromFavorites(int id)
        {
            var userId = UserId;
            var username = Username;
            var favorite = _productService.GetById(id);
            _productService.DeleteFromFavorites(favorite, userId, username);
            return RedirectToAction(nameof(ProductList));
        }
    }
}
