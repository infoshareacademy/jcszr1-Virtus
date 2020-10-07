using BLL;
using System.Collections.Generic;
using System.Linq;

namespace VirtusFitWeb.Services
{
    public class FavoriteService : IFavoriteService
    {
        public List<Product> Favorites = new List<Product>();

        public List<Product> GetAll()
        {
            return Favorites;
        }

        public Product GetById(int id)
        {
            return GetAll().FirstOrDefault(product => product.ProductId == id);
        }

        public void DeleteFromFavorites(Product favorite)
        {
            favorite.IsFavourite = false;
            Favorites.Remove(favorite);
        }

        public void AddToFavorites(Product favorite)
        {
            favorite.IsFavourite = true;
            Favorites.Add(favorite);
        }

    }
}
