using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VirtusFitWeb.Logic;

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
            return View("~/Views/Products/ProductList.cshtml", _productService.GetAll().ToList());
        }
    }
}
