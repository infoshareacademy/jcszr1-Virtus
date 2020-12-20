using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtusFitApi.DTO;
using VirtusFitApi.Models;

namespace VirtusFitApi.DAL
{
    public interface IProductActionsRepository
    {
        List<ProductAction> GetAllProductActions();
        ProductAction GetProductActionById(int id);
        bool AddProductAction(ProductAction action);
        List<SearchStringAction> GetAllSearchStringActions();
        SearchStringAction GetSearchStringActionById(int id);
        bool AddSearchStringAction(SearchStringAction action);
        List<SearchValueAction> GetAllSearchValueActions();
        SearchValueAction GetSearchValueActionById(int id);
        bool AddSearchValueAction(SearchValueAction action);
    }
}
