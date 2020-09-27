using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Mvc;
using VirtusFitWeb.Services;

namespace VirtusFitWeb.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IProductService productService, IFavoriteService favoriteService)
        {
            _productService = productService;
            _favoriteService = favoriteService;
        }
        public IActionResult FavoriteList()
        {
            return View(_favoriteService.GetAll());
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


            _favoriteService.DeleteFromFavorites(favorite);
            return RedirectToAction(nameof(FavoriteList));
        }

    }
}
