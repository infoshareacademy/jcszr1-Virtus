using System.Collections.Generic;
using BLL;

namespace VirtusFitWeb.DAL
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProductById(int productId);
        void InsertProduct(Product product);
        void DeleteProduct(Product product);
        void UpdateProduct(Product product);
        void Save();
    }
}
