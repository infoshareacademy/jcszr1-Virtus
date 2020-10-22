using BLL;
using BLL.Db_Models;
using System.Collections.Generic;
using System.Linq;

namespace VirtusFitWeb.DAL
{
    public class DietPlanRepository : IDietPlanRepository
    {
        private readonly AppContext _context;

        public DietPlanRepository(AppContext context)
        {
            _context = context;
        }

        public List<DietPlan> ListAllDietPlans()
        {
            return _context.DietPlans.ToList();
        }

        public DietPlan GetDietPlanById(int id)
        {
            return _context.DietPlans.Find(id);
        }

        public DailyDietPlan GetDailyDietPlan(int id, int dayNumber)
        {
            return _context.DailyDietPlans.FirstOrDefault(x => x.DietPlanId == id && x.DayNumber == dayNumber);
        }

        public void InsertDietPlan(DietPlan dietPlan)
        {
            _context.DietPlans.Add(dietPlan).Context.SaveChanges();
        }

        public void DeleteDietPlan(DietPlan dietPlan)
        {
            var dailyDietPlansToRemove = ListDailyDietPlans(dietPlan.Id);
            foreach (var daily in dailyDietPlansToRemove)
            {
                _context.DailyDietPlans.Remove(daily).Context.SaveChanges();
            }
            _context.DietPlans.Remove(dietPlan).Context.SaveChanges();
        }

        public void UpdateDietPlan(DietPlan dietPlan)
        {
            _context.DietPlans.Update(dietPlan).Context.SaveChanges();
        }

        public List<DailyDietPlan> ListDailyDietPlans(int id)
        {
            return _context.DailyDietPlans.Where(x => x.DietPlanId == id).ToList();
        }

        public List<ProductInDietPlan> ListProductsInDailyDietPlan(DailyDietPlan dailyDietPlan)
        {
            var productListFromDb = _context.ProductsInDietPlans.Where(x => x.DailyDietPlanId == dailyDietPlan.Id).ToList();
            
            var list = productListFromDb.Select(product => new ProductInDietPlan()
                {
                    Id = product.Id,
                    Product = _context.Products.Find(product.ProductId),
                    NumberOfPortions = product.NumberOfPortions,
                    OrdinalNumber = product.OrdinalNumber,
                    PortionSize = product.PortionSize,
                    TotalCalories = product.TotalCalories
                })
                .ToList();
            return list;
        }

        public void AddDailyDietPlan(DailyDietPlan dailyDietPlan)
        {
            _context.DailyDietPlans.Add(dailyDietPlan).Context.SaveChanges();
        }
        public void UpdateDailyDietPlan(DailyDietPlan dailyDietPlan)
        {
            _context.DailyDietPlans.Update(dailyDietPlan).Context.SaveChanges();
        }
        public void DeleteDailyDietPlan(DailyDietPlan dailyDietPlan)
        {
            var productsToDelete = _context.ProductsInDietPlans.Where(x => x.DailyDietPlanId == dailyDietPlan.Id).ToList();
            foreach (var product in productsToDelete)
            {
                DeleteProductInPlan(product);
            }
            _context.DailyDietPlans.Remove(dailyDietPlan).Context.SaveChanges();
        }

        public ProductInDietPlanDb GetProductFromDailyDietPlan(DailyDietPlan dailyDietPlan, int ordinalNumber)
        {
            return _context.ProductsInDietPlans.FirstOrDefault(x =>
                x.DailyDietPlanId == dailyDietPlan.Id && x.OrdinalNumber == ordinalNumber);
        }

        public List<ProductInDietPlanDb> ListDbProductsInDailyDietPlan(DailyDietPlan dailyDietPlan)
        {
            return _context.ProductsInDietPlans.Where(x => x.DailyDietPlanId == dailyDietPlan.Id).ToList();
        }
        public void AddProductInPlan(ProductInDietPlanDb product)
        {
            _context.ProductsInDietPlans.Add(product).Context.SaveChanges();
        }
        public void UpdateProductInPlan(ProductInDietPlanDb productInDietPlanDb)
        {
            _context.ProductsInDietPlans.Update(productInDietPlanDb).Context.SaveChanges();
        }
        public void DeleteProductInPlan(ProductInDietPlanDb productInDietPlanDb)
        {
            _context.ProductsInDietPlans.Remove(productInDietPlanDb).Context.SaveChanges();
        }
    }
}
