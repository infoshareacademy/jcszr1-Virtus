using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;

namespace VirtusFitWeb.Logic
{
    public class ProductService : IProductService
    {
        public IList<Product> Products = new List<Product>();

        public IList<Product> GetAll()
        {
            var loader = new ProductLoader(); 
            var products = loader.GetProductsFromFile();

            foreach (var product in products)
            {
                Products.Add(product);
            }
            return Products;
        }
    }
}
