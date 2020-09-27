using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;

namespace VirtusFitWeb.Services
{
    public interface IFavoriteService
    {
        List<Product> GetAll();

        Product GetById(int id);

        void DeleteFromFavorites(Product product);

        void AddToFavorites(Product product);
    }
}
