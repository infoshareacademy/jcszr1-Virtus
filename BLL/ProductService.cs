using System.Collections.Generic;

namespace BLL
{
    public static class ProductService
    {
        public static List<Product> listOfProducts = new List<Product>();
        
        public static void AddNewProduct(int id, string productName, string portionUnit, int quantity, int portionQuantity, int energy, double fat, double carbohydrates,  double protein, int sugar, double salt, int fiber )
        {
            Product newProduct = new Product(id, productName, portionUnit, quantity, portionQuantity, energy, fat, carbohydrates, protein, sugar ,salt, fiber); 
            listOfProducts.Add(newProduct);
        }
    }
}
