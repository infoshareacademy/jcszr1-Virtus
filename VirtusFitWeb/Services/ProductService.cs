using BLL;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VirtusFitWeb.Services
{
    public class ProductService : IProductService
    {
        private readonly SearchProductLogic searchProductLogic = new SearchProductLogic();

        public List<Product> GetAll()
        {
            using (ProductContext entities = new ProductContext())
            {
                return entities.Products.ToList();
            }
        }

        public Product GetById(int id)
        {
            return GetAll().FirstOrDefault(product => product.ProductId == id);
        }

        public void DeleteById(int id)
        {
            using (ProductContext entities = new ProductContext())
            {
                var product = entities.Products.Find(id);
                entities.Products.Remove(product);
                entities.SaveChanges();
            }
        }

        public Product Create(Product newProduct)
        {
            using (ProductContext entities = new ProductContext())
            {
                entities.Products.Add(newProduct);
                entities.Entry(newProduct).State = EntityState.Added;
                entities.SaveChanges();
                return newProduct;
            }
        }

        public void Update(int id, Product product)
        {
            using (ProductContext entities = new ProductContext())
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
                productToBeUpdated.IsFavourite = false;
                entities.Entry(productToBeUpdated).State = EntityState.Modified;
                entities.SaveChanges();
            }
        }

        public void DeleteFromFavorites(Product favorite)
        {
            using (ProductContext entities = new ProductContext())
            {
                var fav = entities.Products.Find(favorite.ProductId);
                fav.IsFavourite = false;
                entities.SaveChanges();
            }
        }

        public void AddToFavorites(Product favorite)
        {
            using (ProductContext entities = new ProductContext())
            {
                var fav = entities.Products.Find(favorite.ProductId);
                fav.IsFavourite = true;
                entities.SaveChanges();
            }
        }

        public List<Product> SearchByName(string name)
        {
            return searchProductLogic.SearchByName(Products, name);
        }
        public List<Product> SearchByFat(double minfat, double maxfat)
        {
            return searchProductLogic.SearchByFat(Products, minfat, maxfat);
        }

        public List<Product> SearchByCalories(double minenergy, double maxenergy)
        {
            return searchProductLogic.SearchByCalories(Products, minenergy, maxenergy);
        }
        public List<Product> SearchByCarbohydrates(double mincarb, double maxcarb)
        {
            return searchProductLogic.SearchByCarbohydrates(Products, mincarb, maxcarb);
        }
        public List<Product> SearchByProteins(double minprotein, double maxprotein)
        {
            return searchProductLogic.SearchByProteins(Products, minprotein, maxprotein);
        }
    }
}
