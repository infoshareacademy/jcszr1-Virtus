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
        private static readonly List<Product> _staticListOfProducts = new List<Product>();

        public static List<Product> GetProductsFromFile()
        {
            var path = Environment.CurrentDirectory + "\\food_source.json";
            var jsonString = File.ReadAllText(path);
            SampleProductClass products = JsonConvert.DeserializeObject<SampleProductClass>(jsonString);
            int i = 0;
            foreach (var item in products.Data)
            {
                var internalProductId = Convert.ToInt32(products.Data[i].Id);
                var internalProductName = products.Data[i].DisplayNameTranslations.En;
                int internalEnergy;
                int internalFat;
                int internalCarbohydrates;
                int internalProtein;
                int internalSalt;
                int internalFiber;
                int internalSugar;
                int internalQuantity;
                int internalPortionQuantity;
                string internalPortionUnit;
                try
                {
                    internalEnergy = (int)products.Data[i].Nutrients["energy"].PerHundred;
                }
                catch (Exception e)
                {
                    internalEnergy = 0;
                }
                try
                {
                    internalFat = (int)products.Data[i].Nutrients["fat"].PerHundred;
                }
                catch (Exception e)
                {
                    internalFat = 0;
                }
                try
                {
                    internalCarbohydrates = (int)products.Data[i].Nutrients["carbohydrates"].PerHundred;
                }
                catch (Exception e)
                {
                    internalCarbohydrates = 0;
                }
                try
                {
                    internalProtein = (int)products.Data[i].Nutrients["protein"].PerHundred;
                }
                catch (Exception e)
                {
                    internalProtein = 0;
                }
                try
                {
                    internalSalt = (int)products.Data[i].Nutrients["salt"].PerHundred;
                }
                catch (Exception e)
                {
                    internalSalt = 0;
                }
                try
                {
                    internalFiber = (int) products.Data[i].Nutrients["fiber"].PerHundred;
                }
                catch (Exception e)
                {
                    internalFiber = 0;
                }
                try
                {
                    internalSugar = (int) products.Data[i].Nutrients["sugar"].PerHundred;
                }
                catch (Exception e)
                {
                    internalSugar = 0;
                }
                try
                {
                    internalQuantity = (int)products.Data[i].Quantity;
                }
                catch (Exception e)
                {
                    internalQuantity = 0;
                }
                try
                {
                    internalPortionQuantity = (int)products.Data[i].PortionQuantity;
                }
                catch (Exception e)
                {
                    internalPortionQuantity = 0;
                }
                switch (products.Data[i].Unit)
                    {
                        case PortionUnitEnum.G:
                            internalPortionUnit = "g";
                            break;
                        case PortionUnitEnum.ML:
                            internalPortionUnit = "ml";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                _staticListOfProducts.Add(new Product() { ProductId = internalProductId, ProductName = internalProductName, Energy = internalEnergy, Carbohydrates = internalCarbohydrates, Fat = internalFat, Protein = internalProtein, Salt = internalSalt, Fiber = internalFiber, Sugar = internalSugar, PortionQuantity = internalPortionQuantity, PortionUnit = internalPortionUnit, Quantity = internalQuantity});
                i++;
            }
            return _staticListOfProducts;
        }

    }
}
