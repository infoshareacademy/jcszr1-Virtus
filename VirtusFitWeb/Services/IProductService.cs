using BLL;
using System.Collections.Generic;

namespace VirtusFitWeb.Services
{
    public interface IProductService
    {
        List<Product> GetAll();

        Product GetById(int id);

        void DeleteById(int id);

        Product Create(Product newProduct);

        void Update(int id, Product product);

        void DeleteFromFavorites(Product product);

        void AddToFavorites(Product product);

        List<Product> SearchByName(string name);
    }
}
