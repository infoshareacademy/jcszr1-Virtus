using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;

namespace VirtusFitWeb.Logic
{
    public interface IProductService
    {
        IList<Product> GetAll();

        Product GetById(int id);

        void DeleteById(int id);

        Product Create(Product newProduct);

        void Update(int id, Product product);
    }
}
