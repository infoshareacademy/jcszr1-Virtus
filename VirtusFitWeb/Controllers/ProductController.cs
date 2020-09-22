using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Mvc;
using VirtusFitWeb.Services;
using ProductService = VirtusFitWeb.Services.ProductService;

namespace VirtusFitWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService service)
        {
            _productService = service;
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
    }
}
