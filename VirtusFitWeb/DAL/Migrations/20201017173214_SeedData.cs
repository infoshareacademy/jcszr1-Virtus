using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtusFitWeb.DAL.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Carbohydrates", "Energy", "Fat", "Fiber", "IsFavourite", "PortionQuantity", "PortionUnit", "ProductName", "Protein", "Quantity", "Salt", "Sugar" },
                values: new object[,]
                {
                    { 1, 37.5, 179, 0.20999999999999999, 0, false, 50, "g", "Bio Spaghetti ", 5.5, 500, 0.0, 0 },
                    { 26, 0.0, 0, 0.0, 0, false, 0, "ml", "RAMSEIER : HOCHSTÄMMER, obstsaft naturtrüe", 0.0, 500, 0.0, 0 },
                    { 25, 27.5, 114, 0.5, 0, false, 250, "ml", "RAMSEIER: APPLE JUICE, natural", 0.5, 500, 0.0, 0 },
                    { 24, 22.0, 109, 1.25, 0, false, 250, "ml", "GRANINI: 100% ORANGE", 2.5, 330, 0.0, 0 },
                    { 23, 0.0, 0, 0.0, 0, false, 0, "ml", "granini: My Moment, Lemon-Lemon", 0.0, 330, 0.0, 0 },
                    { 22, 0.0, 0, 0.0, 0, false, 0, "ml", "granini: My Moment, Pink Grapefruit - Cranberry", 0.0, 330, 0.0, 0 },
                    { 21, 0.0, 0, 0.0, 0, false, 0, "ml", "granini: pink grapefruit", 0.0, 1000, 0.0, 0 },
                    { 20, 0.0, 0, 0.0, 0, false, 0, "ml", "granini : Fruchtcocktail", 0.0, 1000, 0.0, 0 },
                    { 19, 0.0, 0, 0.0, 0, false, 0, "ml", "granini: Orange 100%, without pulp", 0.0, 1000, 0.0, 0 },
                    { 18, 0.0, 0, 0.0, 0, false, 0, "ml", "granini: peach", 0.0, 1000, 0.0, 0 },
                    { 17, 0.0, 0, 0.0, 0, false, 0, "ml", "granini: pineapple", 0.0, 1000, 0.0, 0 },
                    { 16, 0.0, 0, 0.0, 0, false, 0, "ml", "granini: orange-mango", 0.0, 1000, 0.0, 0 },
                    { 15, 0.0, 0, 0.0, 0, false, 0, "ml", "Belle France: cider vinegar, Normandy", 0.0, 750, 0.0, 0 },
                    { 14, 0.0, 0, 0.0, 0, false, 0, "g", "Barilla: the Pesti, Calabrian", 0.0, 190, 0.0, 0 },
                    { 13, 0.0, 0, 0.0, 0, false, 0, "g", "Barilla: the Pesti, red pesto", 0.0, 200, 0.0, 0 },
                    { 12, 0.0, 0, 0.0, 0, false, 0, "ml", "Lou mas: Sunflower oil", 0.0, 1000, 0.0, 0 },
                    { 11, 7.0999999999999996, 59, 2.5, 0, false, 100, "g", "PANZANI", 1.3, 400, 1.1799999999999999, 0 },
                    { 10, 0.0, 0, 0.0, 0, false, 15, "g", "Raw health Bio - White Chia Seeds ", 0.0, 450, 0.0, 0 },
                    { 9, 0.0, 0, 0.0, 0, false, 0, "g", "Jean Hervé - Almond Purée", 0.0, 700, 0.0, 0 },
                    { 8, 0.42999999999999999, 103, 9.0299999999999994, 0, false, 43, "g", "Naturaplan Cipollata veal", 5.1600000000000001, 150, 0.72999999999999998, 0 },
                    { 7, 1.5, 184, 16.199999999999999, 4, false, 30, "g", "Naturaplan Bio whole Almonds ", 6.5999999999999996, 200, 0.0, 0 },
                    { 6, 0.80000000000000004, 124, 10.0, 0, false, 50, "g", "Vieniese Sausages", 8.0, 200, 0.90000000000000002, 0 },
                    { 5, 0.0, 0, 0.0, 0, false, 0, "g", "LEAVITT'S AMERICANA, Fancy Mixed Nuts", 0.0, 284, 0.0, 0 },
                    { 4, 0.75, 197, 15.0, 0, false, 75, "g", "Naturaplan Bio Mozzarella ", 15.0, 450, 0.29999999999999999, 0 },
                    { 3, 0.0, 0, 0.0, 0, false, 0, "g", "Caffè Mauro - Roasted organic coffee blend", 0.0, 1000, 0.0, 0 },
                    { 2, 1.1299999999999999, 181, 13.5, 0, false, 75, "g", "Naturaplan Mozzarella", 14.25, 150, 0.38, 0 },
                    { 27, 0.0, 0, 0.0, 0, false, 0, "ml", "granini: Fruity & tingling, apple", 0.0, 500, 0.0, 0 },
                    { 28, 0.0, 0, 0.0, 0, false, 0, "ml", "CRISTALP", 0.0, 500, 0.0, 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 28);
        }
    }
}
