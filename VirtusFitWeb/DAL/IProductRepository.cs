using BLL;
using System.Collections.Generic;

namespace VirtusFitWeb.DAL
{
    public interface IProductRepository
    {
        List<Product> GetProducts(string userId);
        Product GetProductById(int productId);
        void InsertProduct(Product product);
        void DeleteProduct(Product product);
        void UpdateProduct(Product product);
        void Save();
    }
}
