using BLL;
using System.Collections.Generic;
using System.Linq;
using IProductRepository = VirtusFitWeb.DAL.IProductRepository;

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
            return _repository.GetProducts().Where(product => product.IsFavorite).ToList();
        }

        public Product GetById(int id)
        {
            return GetAll().FirstOrDefault(product => product.ProductId == id);
        }

        public void DeleteFromFavorites(Product favorite)
        {
            var fav = _repository.GetProductById(favorite.ProductId);
            fav.IsFavorite = false;
            _repository.UpdateProduct(fav);
            _repository.Save();
        }

        public void AddToFavorites(Product favorite)
        {
            var fav = _repository.GetProductById(favorite.ProductId);
            fav.IsFavorite = true;
            _repository.UpdateProduct(fav);
            _repository.Save();
        }
    }
}
