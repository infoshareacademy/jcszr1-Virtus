using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtusFitApi.Models;

namespace VirtusFitApi.DAL
{
    public class PlanActionsRepository : IPlanActionsRepository
    {
        private readonly ApiContext _context;

        public PlanActionsRepository(ApiContext context)
        {
            _context = context;
        }

        public List<DietPlanAction> GetAllDietPlanActions()
        {
            return _context.DietPlanActions.ToList();
        }

        public DietPlanAction GetDietPlanActionById(int id)
        {
            var action = _context.DietPlanActions.FirstOrDefault(plan => plan.Id == id);
            return action;
        }

        public bool AddDietPlanAction(DietPlanAction action)
        {
            _context.DietPlanActions.Add(action);
            _context.SaveChanges();
            return true;
        }

        public List<ProductInPlanAction> GetAllProductInPlanActions()
        {
            return _context.ProductInPlanActions.ToList();
        }

        public ProductInPlanAction GetProductInPlanActionById(int id)
        {
            var action = _context.ProductInPlanActions.FirstOrDefault(plan => plan.Id == id);
            return action;
        }

        public bool AddProductInPlanAction(ProductInPlanAction action)
        {
            _context.ProductInPlanActions.Add(action);
            _context.SaveChanges();
            return true;
        }

        public List<BmiAction> GetAllBmiActions()
        {
            return _context.BmiActions.ToList();
        }

        public BmiAction GetBmiActionById(int id)
        {
            var action = _context.BmiActions.FirstOrDefault(action => action.Id == id);
            return action;
        }

        public bool AddBmiAction(BmiAction action)
        {
            _context.BmiActions.Add(action);
            _context.SaveChanges();
            return true;
        }

    }
}
