using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using BLL;


namespace BLL
{
    public class ProductLoader
    {
        public static List<Product> productsFromJson = new List<Product>();
        public static void GetProductsFromFile()
        {
            var path = Environment.CurrentDirectory + "\\food_source.json";
            var jsonString = File.ReadAllText(path);
            dynamic deserializedJson = JsonConvert.DeserializeObject(jsonString);
            foreach (var product in deserializedJson.data)
            {
                productsFromJson.Add(new Product
                {
                    ProductId = product.id,
                    ProductName = product.display_name_translations.en,
                    Energy = product.nutrients?.energy?.per_portion == null ? 0 : product.nutrients?.energy?.per_portion,
                    Fat = product.nutrients?.fat?.per_portion == null ? 0 : product.nutrients?.fat?.per_portion,
                    Carbohydrates = product.nutrients?.carbohydrates?.per_portion == null ? 0 : product.nutrients?.carbohydrates?.per_portion,
                    Protein = product.nutrients?.protein?.per_portion == null ? 0 : product.nutrients?.protein?.per_portion,
                    Salt = product.nutrients?.salt?.per_portion == null ? 0 : product.nutrients?.salt?.per_portion,
                    Fiber = product.nutrients?.fiber?.per_portion == null ? 0 : product.nutrients?.fiber?.per_portion,
                    Sugar = product.nutrients?.sugar?.per_portion == null ? 0 : product.nutrients?.sugar?.per_portion,
                    Quantity = product.quantity == null ? 0 : product.quantity,
                    PortionQuantity = product.portion_quantity == null ? 0 : product.portion_quantity,
                    PortionUnit = product.portion_unit == null ? 0 : product.portion_unit,
                });

            }

        }
    }
}
