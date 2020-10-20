using BLL;
using System.Collections.Generic;
using System.Linq;
using BLL.DAL;
using VirtusFitWeb.Models;

namespace VirtusFitWeb.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IProductRepository _repository;

        public FavoriteService(IProductRepository repository)
        {
            _repository = repository;
        }
        public List<Product> GetAll()
        {
            return _repository.GetProducts().Where(product => product.IsFavourite).ToList();
        }

        public Product GetById(int id)
        {
            return GetAll().FirstOrDefault(product => product.ProductId == id);
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
    }
}
