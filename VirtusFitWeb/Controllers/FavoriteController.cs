using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VirtusFitWeb.Services;

namespace VirtusFitWeb.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFavoriteService _favoriteService;

        public string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
        public string Username => User.FindFirstValue(ClaimTypes.Name);

        public FavoriteController(IProductService productService, IFavoriteService favoriteService)
        {
            _productService = productService;
            _favoriteService = favoriteService;
        }
        public IActionResult FavoriteList()
        {
            return View(_favoriteService.GetAll(UserId));
        }
        [HttpGet]
        public IActionResult FavoriteDetails(int id)
        {
            var favorite = _favoriteService.GetById(id);
            return View(favorite);
        }

        [HttpGet]
        public IActionResult DeleteFromFavorites(int id)
        {
            var favorite = _productService.GetById(id);
            var userId = UserId;
            var username = Username;


            _favoriteService.DeleteFromFavorites(favorite, userId, username);
            return RedirectToAction(nameof(FavoriteList));
        }

    }
}
