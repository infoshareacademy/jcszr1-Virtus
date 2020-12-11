using BLL;
using System.Collections.Generic;
using System.Linq;

namespace VirtusFitWeb.DAL
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppContext _context;

        public ProductRepository(AppContext context)
        {
            _context = context;
        }

        public List<Product> GetProducts(string userId)
        {
            return _context.Products.Where(p=>p.UserId == userId).ToList();
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.Find(productId);
        }

        public void InsertProduct(Product product)
        {
            _context.Products.Add(product).Context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product).Context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product).Context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
