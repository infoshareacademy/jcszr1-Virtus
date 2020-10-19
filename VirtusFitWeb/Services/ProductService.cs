using BLL;
using System.Collections.Generic;
using System.Linq;
using BLL.DAL;
using Microsoft.EntityFrameworkCore;

namespace VirtusFitWeb.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly SearchProductLogic _searchProductLogic = new SearchProductLogic();

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public List<Product> GetAll()
        {
            return _repository.GetProducts();
        }

        public Product GetById(int id)
        { 
            return _repository.GetProductById(id);
        }

        public void DeleteById(int id)
        {
            var product = GetById(id);
            _repository.DeleteProduct(product);
        }

        public Product Create(Product newProduct)
        {
            _repository.InsertProduct(newProduct);
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
                productToBeUpdated.IsFavourite = false;
                _repository.UpdateProduct(productToBeUpdated);
        }

        public void DeleteFromFavorites(Product favorite)
        {
                var fav = _repository.GetProductById(favorite.ProductId);
                fav.IsFavourite = false;
                _repository.UpdateProduct(fav);
                _repository.Save();
        }

        public void AddToFavorites(Product favorite)
        {

                var fav = _repository.GetProductById(favorite.ProductId);
                fav.IsFavourite = true;
                _repository.UpdateProduct(fav);
                _repository.Save();
        }

        public List<Product> SearchByName(string name)
        {
            return _searchProductLogic.SearchByName(_repository.GetProducts(), name);
        }
        public List<Product> SearchByFat(double minfat, double maxfat)
        {
            return _searchProductLogic.SearchByFat(_repository.GetProducts(), minfat, maxfat);
        }
        public List<Product> SearchByCalories(double minenergy, double maxenergy)
        {
            return _searchProductLogic.SearchByCalories(_repository.GetProducts(), minenergy, maxenergy);
        }
        public List<Product> SearchByCarbohydrates(double mincarb, double maxcarb)
        {
            return _searchProductLogic.SearchByCarbohydrates(_repository.GetProducts(), mincarb, maxcarb);
        }
        public List<Product> SearchByProteins(double minprotein, double maxprotein)
        {
            return _searchProductLogic.SearchByProteins(_repository.GetProducts(), minprotein, maxprotein);
        }
    }
}
