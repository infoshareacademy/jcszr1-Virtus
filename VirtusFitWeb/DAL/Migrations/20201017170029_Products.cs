using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtusFitWeb.DAL.Migrations
{
    public partial class Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Energy = table.Column<int>(type: "int", nullable: false),
                    Fat = table.Column<double>(type: "float", nullable: false),
                    Carbohydrates = table.Column<double>(type: "float", nullable: false),
                    Protein = table.Column<double>(type: "float", nullable: false),
                    Salt = table.Column<double>(type: "float", nullable: false),
                    Fiber = table.Column<int>(type: "int", nullable: false),
                    Sugar = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PortionQuantity = table.Column<int>(type: "int", nullable: false),
                    PortionUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFavourite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
