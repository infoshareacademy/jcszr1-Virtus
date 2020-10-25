using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BLL
{
    public class ProductContext : DbContext
    {
        private static readonly string ConnectionString = @"Server=(localdb)\MSSQLLocalDB;Database=VirtusFitDB;Trusted_Connection=True;";

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var seedData = ProductLoader.GetProductsFromFile();
            foreach (var product in seedData)
            {
                modelBuilder.Entity<Product>().HasData(new Product
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Energy = product.Energy,
                    Fat = product.Fat,
                    Carbohydrates = product.Carbohydrates,
                    Protein = product.Protein,
                    Salt = product.Salt,
                    Fiber = product.Fiber,
                    Sugar = product.Sugar,
                    Quantity = product.Quantity,
                    PortionQuantity = product.PortionQuantity,
                    PortionUnit = product.PortionUnit,
                    IsFavourite = product.IsFavourite
                });
            }
        }
    }
}
