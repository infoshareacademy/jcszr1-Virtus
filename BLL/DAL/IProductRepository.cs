using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProductById(int productId);
        void InsertProduct(Product product, bool commit = true);
        void DeleteProduct(Product product, bool commit = true);
        void UpdateProduct(Product product, bool commit = true);
        Task Save();
    }
}
