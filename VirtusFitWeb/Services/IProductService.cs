using BLL;
using System.Collections.Generic;

namespace VirtusFitWeb.Services
{
    public interface IProductService
    {
        List<Product> GetAll(string userId);

        Product GetById(int id);

        void DeleteById(int id, string userId, string username);

        Product Create(Product newProduct, string userId, string username);

        void Update(int id, Product product, string userId, string username);

        void DeleteFromFavorites(Product product, string userId, string username);

        void AddToFavorites(Product product, string userId, string username);

        List<Product> SearchByName(string name, string userId, string username);
        List<Product> SearchByFat(double minfat, double maxfat, string userId, string username);
        List<Product> SearchByCalories(double minenergy, double maxenergy, string userId, string username);
        List<Product> SearchByCarbohydrates(double mincarb, double maxcarb, string userId, string username);
        List<Product> SearchByProteins(double minprotein, double maxeprotein, string userId, string username);


    }
}