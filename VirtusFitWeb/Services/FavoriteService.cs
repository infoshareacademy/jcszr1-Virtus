using BLL;
using System.Collections.Generic;
using System.Linq;
using VirtusFitWeb.Models;

namespace VirtusFitWeb.Services
{
    public class FavoriteService : IFavoriteService
    {
        public List<Product> GetAll()
        {
            using (ProductContext entities = new ProductContext())
            {
                return entities.Products.Where(product => product.IsFavourite).ToList();
            }
        }

        public Product GetById(int id)
        {
            using (ProductContext entities = new ProductContext())
            {
                return GetAll().FirstOrDefault(product => product.ProductId == id);
            }
        }

        public void DeleteFromFavorites(Product favorite)
        {
            using (ProductContext entities = new ProductContext())
            {
                if (favorite != null)
                {
                    var fav = entities.Products.Find(favorite.ProductId);
                    fav.IsFavourite = false;
                    entities.SaveChanges();
                }
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
    }
}
