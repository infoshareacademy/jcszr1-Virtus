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

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public List<Product> GetProducts()
        {
            var list = GetProductsAsync().Result;
            return list;
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.Find(productId);
        }

        public async Task InsertProduct(Product product, bool commit = true)
        {

            await _context.Products.AddAsync(product);

            if (commit)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async void DeleteProduct(Product product, bool commit = true)
        {
            _context.Products.Remove(product).Context.SaveChanges();

            if (commit)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async void UpdateProduct(Product product, bool commit = true)
        {
            _context.Products.Update(product).Context.SaveChanges();

            if (commit)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
