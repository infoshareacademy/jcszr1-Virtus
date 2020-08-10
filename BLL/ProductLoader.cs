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
<<<<<<< HEAD
        public static readonly List<Product> _staticListOfProducts = new List<Product>
        {
           // new Product {ProductId = 1, ProductName = "something1", ProductWeight = 2, Energy = 1, Carbohydrates = 32, Fat = 5, Protein = 61, Salt = 8},
           // new Product {ProductId = 2, ProductName = "something2", ProductWeight = 1, Energy = 9, Carbohydrates = 84, Fat = 75, Protein = 8, Salt = 20},
           // new Product {ProductId = 3, ProductName = "something3", ProductWeight = 7, Energy = 5, Carbohydrates = 99, Fat = 535, Protein = 23, Salt = 1},
           // new Product {ProductId = 4, ProductName = "something4", ProductWeight = 8, Energy = 90, Carbohydrates = 16, Fat = 15, Protein = 78, Salt = 3},
           // new Product {ProductId = 5, ProductName = "something5", ProductWeight = 9, Energy = 11, Carbohydrates = 3, Fat = 25, Protein = 120, Salt = 66},
        };

      //  public static List<Product> GetProductsFromFile()
        //
        //  var jsonString = File.ReadAllText(@"food_source.json");
        //  SampleProductClass products = JsonConvert.DeserializeObject<SampleProductClass>(jsonString);
        //  int i = 0;
        //  foreach (var item in products.Data)
        //  {
        //      var internalProductId = Convert.ToInt32(products.Data[i].Id);
        //      var internalProductName = products.Data[i].DisplayNameTranslations.En;
        //      var internalProductWeight = Convert.ToInt32(products.Data[i].PortionQuantity);
        //      int internalEnergy;
        //      int internalFat;
        //      int internalCarbohydrates;
        //      int internalProtein;
        //      int internalSalt;
        //      int internalFiber;
        //      int internalSugar;
        //      int internalQuantity;
        //      int internalPortionQuantity;
        //      string internalPortionUnit;
        //      try
        //      {
        //          internalEnergy = (int)products.Data[i].Nutrients["energy"].PerHundred;
        //      }
        //      catch (Exception e)
        //      {
        //          internalEnergy = 0;
        //      }
        //      try
        //      {
        //          internalFat = (int)products.Data[i].Nutrients["fat"].PerHundred;
        //      }
        //      catch (Exception e)
        //      {
        //          internalFat = 0;
        //      }
        //      try
        //      {
        //          internalCarbohydrates = (int)products.Data[i].Nutrients["carbohydrates"].PerHundred;
        //      }
        //      catch (Exception e)
        //      {
        //          internalCarbohydrates = 0;
        //      }
        //      try
        //      {
        //          internalProtein = (int)products.Data[i].Nutrients["protein"].PerHundred;
        //      }
        //      catch (Exception e)
        //      {
        //          internalProtein = 0;
        //      }
        //      try
        //      {
        //          internalSalt = (int)products.Data[i].Nutrients["salt"].PerHundred;
        //      }
        //      catch (Exception e)
        //      {
        //          internalSalt = 0;
        //      }
        //      try
        //      {
        //          internalFiber = (int) products.Data[i].Nutrients["fiber"].PerHundred;
        //      }
        //      catch (Exception e)
        //      {
        //          internalFiber = 0;
        //      }
        //      try
        //      {
        //          internalSugar = (int) products.Data[i].Nutrients["sugar"].PerHundred;
        //      }
        //      catch (Exception e)
        //      {
        //          internalSugar = 0;
        //      }
        //      try
        //      {
        //          internalQuantity = (int)products.Data[i].Quantity;
        //      }
        //      catch (Exception e)
        //      {
        //          internalQuantity = 0;
        //      }
        //      try
        //      {
        //          internalPortionQuantity = (int)products.Data[i].PortionQuantity;
        //      }
        //      catch (Exception e)
        //      {
        //          internalPortionQuantity = 0;
        //      }
        //      switch (products.Data[i].Unit)
        //          {
        //              case PortionUnitEnum.G:
        //                  internalPortionUnit = "G";
        //                  break;
        //              case PortionUnitEnum.ML:
        //                  internalPortionUnit = "ML";
        //                  break;
        //              default:
        //                  throw new ArgumentOutOfRangeException();
        //          }
        //      _staticListOfProducts.Add(new Product() { ProductId = internalProductId, ProductName = internalProductName, Energy = internalEnergy, Carbohydrates = internalCarbohydrates, Fat = internalFat, Protein = internalProtein, Salt = internalSalt, Fiber = internalFiber, Sugar = internalSugar, PortionQuantity = internalPortionQuantity, PortionUnit = internalPortionUnit, Quantity = internalQuantity});
        //      i++;
        //  }
        //  return _staticListOfProducts;
=======
        private static readonly List<Product> _staticListOfProducts = new List<Product>();

        public static List<Product> GetProductsFromFile()
        {
            var jsonString = File.ReadAllText(@"food_source.json");
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
>>>>>>> master
        }

    }


