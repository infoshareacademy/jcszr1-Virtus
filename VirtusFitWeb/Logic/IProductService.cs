using BLL;
using System.Collections.Generic;

namespace VirtusFitWeb.Logic
{
    public interface IProductService
    {
        List<Product> GetAll();

        Product GetById(int id);

        void DeleteById(int id);

        Product Create(Product newProduct);

        void Update(int id, Product product);
    }
}
