using BLL;
using System.Collections.Generic;

namespace VirtusFitWeb.Services
{
    public interface IFavoriteService
    {
        List<Product> GetAll(string userId);

        Product GetById(int id);

        void DeleteFromFavorites(Product product, string userid, string username);

        void AddToFavorites(Product product, string userid, string username);
    }
}
