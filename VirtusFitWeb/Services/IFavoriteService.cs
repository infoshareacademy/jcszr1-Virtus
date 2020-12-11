using BLL;
using System.Collections.Generic;

namespace VirtusFitWeb.Services
{
    public interface IFavoriteService
    {
        List<Product> GetAll(string userId);

        Product GetById(int id);

        void DeleteFromFavorites(Product product);

        void AddToFavorites(Product product);
    }
}
