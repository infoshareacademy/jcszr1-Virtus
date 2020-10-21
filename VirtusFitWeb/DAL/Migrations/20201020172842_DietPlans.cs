using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtusFitWeb.DAL.Migrations
{
    public partial class DietPlans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DietPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaloriesPerDay = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyDietPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayNumber = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaloriesSum = table.Column<int>(type: "int", nullable: false),
                    FatSum = table.Column<double>(type: "float", nullable: false),
                    CarbohydratesSum = table.Column<double>(type: "float", nullable: false),
                    ProteinSum = table.Column<double>(type: "float", nullable: false),
                    DietPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyDietPlan", x => x.DietPlanId);
                    table.ForeignKey(
                        name: "FK_DailyDietPlan_DietPlans_DietPlanId1",
                        column: x => x.DietPlanId,
                        principalTable: "DietPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductInDietPlan",
                columns: table => new
                {
                    OrdinalNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    PortionSize = table.Column<int>(type: "int", nullable: false),
                    NumberOfPortions = table.Column<int>(type: "int", nullable: false),
                    TotalCalories = table.Column<int>(type: "int", nullable: false),
                    DailyDietPlanDietPlanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInDietPlan", x => x.OrdinalNumber);
                    table.ForeignKey(
                        name: "FK_ProductInDietPlan_DailyDietPlan_DailyDietPlanDietPlanId",
                        column: x => x.DailyDietPlanDietPlanId,
                        principalTable: "DailyDietPlan",
                        principalColumn: "DietPlanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductInDietPlan_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyDietPlan_DietPlanId1",
                table: "DailyDietPlan",
                column: "DietPlanId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInDietPlan_DailyDietPlanDietPlanId",
                table: "ProductInDietPlan",
                column: "DailyDietPlanDietPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInDietPlan_ProductId",
                table: "ProductInDietPlan",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductInDietPlan");

            migrationBuilder.DropTable(
                name: "DailyDietPlan");

            migrationBuilder.DropTable(
                name: "DietPlans");
        }
    }
}
