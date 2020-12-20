using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VirtusFitApi.DTO;
using VirtusFitApi.Models;

namespace VirtusFitApi.DAL
{
    public class ProductActionsRepository : IProductActionsRepository
    {
        private readonly ApiContext _context;

        public ProductActionsRepository(ApiContext context)
        {
            _context = context;
        }
        public List<ProductAction> GetAllProductActions()
        {
            return _context.ProductActions.ToList();
        }

        public ProductAction GetProductActionById(int id)
        {
            var action = _context.ProductActions.FirstOrDefault(actionId => actionId.Id==id);
            return action;
        }

        public bool AddProductAction(ProductAction action)
        {
            _context.ProductActions.Add(action);
            _context.SaveChanges();
            return true;
        }

        public List<SearchStringAction> GetAllSearchStringActions()
        {
            return _context.SearchStringActions.ToList();
        }

        public SearchStringAction GetSearchStringActionById(int id)
        {
            var action = _context.SearchStringActions.FirstOrDefault(actionId => actionId.Id == id);
            return action;
        }

        public bool AddSearchStringAction(SearchStringAction action)
        {
            _context.SearchStringActions.Add(action);
            _context.SaveChanges();
            return true;
        }

        public List<SearchValueAction> GetAllSearchValueActions()
        {
            return _context.SearchValueActions.ToList();
        }

        public SearchValueAction GetSearchValueActionById(int id)
        {
            var action = _context.SearchValueActions.FirstOrDefault(actionId => actionId.Id == id);
            return action; 
        }

        public bool AddSearchValueAction(SearchValueAction action)
        {
            _context.SearchValueActions.Add(action);
            _context.SaveChanges();
            return true;
        }
    }
}
