using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using BLL;

namespace VirtusFitWeb.Logic
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
            Favorites.Remove(favorite);
        }

        public void AddToFavorites(Product favorite)
        {
            Favorites.Add(favorite);
        }

    }
}
