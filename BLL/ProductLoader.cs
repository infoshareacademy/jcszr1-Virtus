using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace BLL
{
    public class ProductLoader
    {
        public static List<Product> GetProductsFromFile()
        {
            List<Product> productsFromJson = new List<Product>();
            var path = Environment.CurrentDirectory + "\\food_source.json";
            var jsonString = File.ReadAllText(path);
            dynamic deserializedJson = JsonConvert.DeserializeObject(jsonString);
            foreach (var product in deserializedJson.data)
            {
                productsFromJson.Add(new Product
                {
                    ProductId = productsFromJson.Count + 1,
                    ProductName = product.display_name_translations.en,
                    Energy = product.nutrients?.energy_kcal?.per_portion == null ? 0 : product.nutrients?.energy_kcal?.per_portion,
                    Fat = product.nutrients?.fat?.per_portion == null ? 0 : product.nutrients?.fat?.per_portion,
                    Carbohydrates = product.nutrients?.carbohydrates?.per_portion == null ? 0 : product.nutrients?.carbohydrates?.per_portion,
                    Protein = product.nutrients?.protein?.per_portion == null ? 0 : product.nutrients?.protein?.per_portion,
                    Salt = product.nutrients?.salt?.per_portion == null ? 0 : product.nutrients?.salt?.per_portion,
                    Fiber = product.nutrients?.fiber?.per_portion == null ? 0 : product.nutrients?.fiber?.per_portion,
                    Sugar = product.nutrients?.sugar?.per_portion == null ? 0 : product.nutrients?.sugar?.per_portion,
                    Quantity = product.quantity == null ? 0 : product.quantity,
                    PortionQuantity = product.portion_quantity == null ? 0 : product.portion_quantity,
                    PortionUnit = product.portion_unit == null ? 0 : product.portion_unit,
                }
                );
            }

            foreach (var product in productsFromJson)
            {
                product.PortionUnit = product.PortionUnit.ToLower();
            }

            return productsFromJson;
        }
    }
}
