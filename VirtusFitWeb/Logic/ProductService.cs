using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Mvc;

namespace VirtusFitWeb.Logic
{
    public class ProductService : IProductService
    {
        public List<Product> Products = BLL.ProductLoader.GetProductsFromFile();

        public IList<Product> GetAll()
        {

            return Products;
        }

        public Product GetById(int id)
        {

            return GetAll().FirstOrDefault(product => product.ProductId == id);
        }

        public void DeleteById(int id)
        {
            var product = GetById(id);
            Products.Remove(product);
        }

        public Product Create(Product newProduct)
        {
            newProduct.ProductId = Products.Max(product => product.ProductId) + 1;
            Products.Add(newProduct);
            return newProduct;
        }

        public void Update(int id, Product product)
        {
            var productToBeUpdated = GetById(id);
            productToBeUpdated.ProductName = product.ProductName;
            productToBeUpdated.Energy = product.Energy;
            productToBeUpdated.Fat = product.Fat;
            productToBeUpdated.Carbohydrates = product.Carbohydrates;
            productToBeUpdated.Protein = product.Protein;
            productToBeUpdated.Salt = product.Salt;
            productToBeUpdated.Fiber = product.Fiber;
            productToBeUpdated.Sugar = product.Sugar;
            productToBeUpdated.Quantity = product.Quantity;
            productToBeUpdated.PortionQuantity = product.PortionQuantity;
            productToBeUpdated.PortionUnit = product.PortionUnit;
        }

    }
}
