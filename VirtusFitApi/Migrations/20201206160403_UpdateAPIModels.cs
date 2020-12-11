using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtusFitApi.Migrations
{
    public partial class UpdateAPIModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DailyDietPlanId",
                table: "ProductInPlanActions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DietPlanId",
                table: "ProductInPlanActions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "ProductInPlanActions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "ProductActions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "DietPlanActions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "DietPlanActions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalCalories",
                table: "DailyDietPlanActions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "DailyDietPlanActions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyDietPlanId",
                table: "ProductInPlanActions");

            migrationBuilder.DropColumn(
                name: "DietPlanId",
                table: "ProductInPlanActions");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "ProductInPlanActions");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "ProductActions");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "DietPlanActions");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "DietPlanActions");

            migrationBuilder.DropColumn(
                name: "TotalCalories",
                table: "DailyDietPlanActions");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "DailyDietPlanActions");
        }
    }
}
