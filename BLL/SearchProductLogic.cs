using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class SearchProductLogic
    {
        public List<Product> SearchByName(List<Product> productList, string searchValue)
        {
            return productList.Where(product =>
                product.ProductName.Contains(searchValue, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public List<Product> SearchByCalories(List<Product> productList, double searchValue)
        {
            return productList.Where(product => product.Energy == searchValue).ToList();
        }

        public List<Product> SearchByCalories(List<Product> productList, double minValue, double maxValue)
        {
            return productList.Where(product => product.Energy >= minValue && product.Energy <= maxValue).ToList();
        }

        public List<Product> SearchByFat(List<Product> productList, double searchValue)
        {
            return productList.Where(product => product.Fat == searchValue).ToList();
        }

        public List<Product> SearchByFat(List<Product> productList, double minValue, double maxValue)
        {
            return productList.Where(product => product.Fat >= minValue && product.Fat <= maxValue).ToList();
        }

        public List<Product> SearchByCarbohydrates(List<Product> productList, double searchValue)
        {
            return productList.Where(product => product.Carbohydrates == searchValue).ToList();
        }

        public List<Product> SearchByCarbohydrates(List<Product> productList, double minValue, double maxValue)
        {
            return productList.Where(product => product.Carbohydrates >= minValue && product.Carbohydrates <= maxValue).ToList();
        }
        public List<Product> SearchByProteins(List<Product> productList, double searchValue)
        {
            return productList.Where(product => product.Protein == searchValue).ToList();
        }

        public List<Product> SearchByProteins(List<Product> productList, double minValue, double maxValue)
        {
            return productList.Where(product => product.Protein >= minValue && product.Protein <= maxValue).ToList();
        }
    }
}
