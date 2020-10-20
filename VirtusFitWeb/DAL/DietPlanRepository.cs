using System.Collections.Generic;
using System.Linq;
using BLL;

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
            return _context.DietPlans.ToList(); ;
        }

        public DietPlan GetDietPlanById(int id)
        {
            return _context.DietPlans.Find(id);
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
            var dailyDietPlansToRemove = ListDailyDietPlans(dietPlan.Id);
            foreach (var daily in dailyDietPlansToRemove)
            {
                _context.DailyDietPlans.Remove(daily).Context.SaveChanges();
            }
            _context.DietPlans.Update(dietPlan).Context.SaveChanges();
        }

        public List<DailyDietPlan> ListDailyDietPlans(int id)
        {
            return _context.DailyDietPlans.Where(x => x.DietPlanId == id).ToList();
        }
    }
}
