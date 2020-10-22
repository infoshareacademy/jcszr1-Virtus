using BLL;
using BLL.Db_Models;
using Microsoft.EntityFrameworkCore;

namespace VirtusFitWeb.DAL
{
    public class AppContext : DbContext
    {

        private static readonly string ConnectionString = @"Server=(localdb)\MSSQLLocalDB;Database=VirtusFitDB;Trusted_Connection=True;";

        public DbSet<Product> Products { get; set; }
        public DbSet<DietPlan> DietPlans { get; set; }
        public DbSet<DailyDietPlan> DailyDietPlans { get; set; }
        public DbSet<ProductInDietPlanDb> ProductsInDietPlans { get; set; }


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
