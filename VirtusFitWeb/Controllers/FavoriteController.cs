using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Mvc;
using VirtusFitWeb.Logic;

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
        public IActionResult AddToFavorites(int id)
        {
            var favorite = _productService.GetById(id);
            if(_favoriteService.GetAll().Contains(favorite))
            {
                return RedirectToAction(nameof(FavoriteList));
            }
            return View(favorite);
        }

        [HttpPost]
        public IActionResult AddToFavorites(int id, Product product)
        {
            try
            {
                var newFavorite = _productService.GetById(id);
                _favoriteService.AddToFavorites(newFavorite);
                return RedirectToAction(nameof(FavoriteList));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult DeleteFromFavorites(int id)
        {
            var favorite = _productService.GetById(id);
            return View(favorite);
        }

        [HttpPost]
        public IActionResult DeleteFromFavorites(int id, Product product)
        {
            try
            {
                var deleteFavorite = _productService.GetById(id);
                _favoriteService.DeleteFromFavorites(deleteFavorite);
                return RedirectToAction(nameof(FavoriteList));
            }
            catch
            {
                return View();
            }
        }
    }
}
