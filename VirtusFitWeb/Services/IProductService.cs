using BLL;
using System.Collections.Generic;

namespace VirtusFitWeb.Services
{
    public interface IProductService
    {
        List<Product> GetAll(string userId);

        Product GetById(int id);

        void DeleteById(int id, string userId);

        Product Create(Product newProduct, string userId);

        void Update(int id, Product product, string userId);

        void DeleteFromFavorites(Product product);

        void AddToFavorites(Product product);

        List<Product> SearchByName(string name, string userId);
        List<Product> SearchByFat(double minfat, double maxfat, string userId);
        List<Product> SearchByCalories(double minenergy, double maxenergy, string userId);
        List<Product> SearchByCarbohydrates(double mincarb, double maxcarb, string userId);
        List<Product> SearchByProteins(double minprotein, double maxeprotein, string userId);


    }
}