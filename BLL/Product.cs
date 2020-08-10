using System;
using System.Collections.Generic;


namespace BLL
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Energy { get; set; }
        public double Fat { get; set; }
        public double Carbohydrates { get; set; }
        public double Protein { get; set; }
        public double Salt { get; set; }
        public int Fiber { get; set; }
        public int Sugar { get; set; }
        public int Quantity { get; set; }
        public int PortionQuantity { get; set; }
        public string PortionUnit { get; set; }
        public Product(int productId, string productName, string portionUnit, int quantity,  int portionQuantity, int energy, double fat, double carbohydrates, double protein, int sugar, double salt, int fiber)
        {
            this.ProductId = productId;
           this.ProductName = productName;
           this.PortionUnit = portionUnit;
           this.Quantity = quantity;
           this.PortionQuantity = portionQuantity;
           this.Energy = energy;
           this.Fat = fat;
           this.Carbohydrates = carbohydrates;
           this.Protein = protein;
           this.Salt = salt;
           this.Fiber = fiber;
           this.Sugar = sugar;
        }
        //            Product newProduct = new Product(id, productName, portionUnit, quantity, portionQuantity, energy, fat, carbohydrates, protein, salt, fiber, sugar); 

        //id, productName, quantity, portionQuantity, energy, fat, carbohydrates, protein, salt, fiber, sugar
        public Product()
        {
        }

    }
}

