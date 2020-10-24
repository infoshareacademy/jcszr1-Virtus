using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace BLL.DAL
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppContext _context;

        public ProductRepository(AppContext context)
        {
            _context = context;
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList(); ;
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.Find(productId);
        }

        public void InsertProduct(Product product, bool commit = true)
        {
            _context.Products.Add(product).Context.SaveChanges();
        }

        public void DeleteProduct(Product product, bool commit = true)
        {
            _context.Products.Remove(product).Context.SaveChanges();
        }

        public void UpdateProduct(Product product, bool commit = true)
        {
            _context.Products.Update(product).Context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }
    }
}
