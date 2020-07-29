using System;
using System.Collections.Generic;

namespace BLL
{
    public class SearchProductLogic
    {
        public List<Product> SearchByName(List<Product> productList, string searchValue)
        {
            var returnList = new List<Product>();
            
            foreach (var product in productList)
            {
                if (product.productName.Contains(searchValue, StringComparison.InvariantCultureIgnoreCase))
                {
                    returnList.Add(product);
                }
            }
            return returnList;
        }
        public List<Product> SearchByCalories(List<Product> productList, int searchValue)
        {

            var returnList = new List<Product>();

            foreach (var product in productList)
            {
                if (product.energy == searchValue)
                {
                    returnList.Add(product);
                }
            }
            return returnList;
        }

        public List<Product> SearchByCalories(List<Product> productList, int minValue, int maxValue)
        {
            var returnList = new List<Product>();

            foreach (var product in productList)
            {
                if (product.energy >= minValue && product.energy <= maxValue)
                {
                    returnList.Add(product);
                }
            }
            return returnList;
        }
    }
}