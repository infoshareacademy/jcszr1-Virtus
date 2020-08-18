using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BLL
{
    public static class ProductService
    {
        public static List<Product> AddNewProduct(int id, string productName, string portionUnit, int quantity, int portionQuantity, int energy, double fat, double carbohydrates,  double protein, int sugar, double salt, int fiber , List<Product> listOfProducts)
        {
            Product newProduct = new Product(id, productName, portionUnit, quantity, portionQuantity, energy, fat, carbohydrates, protein, sugar ,salt, fiber);
            listOfProducts.Add(newProduct);
            return listOfProducts;
        }
    }
}
