using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtusFitApi.Migrations
{
    public partial class PlanActionsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyDietPlanId",
                table: "DietPlanActions");

            migrationBuilder.RenameColumn(
                name: "PlanId",
                table: "DailyDietPlanActions",
                newName: "DietPlanId");

            migrationBuilder.AddColumn<int>(
                name: "DailyDietPlanId",
                table: "DailyDietPlanActions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyDietPlanId",
                table: "DailyDietPlanActions");

            migrationBuilder.RenameColumn(
                name: "DietPlanId",
                table: "DailyDietPlanActions",
                newName: "PlanId");

            migrationBuilder.AddColumn<int>(
                name: "DailyDietPlanId",
                table: "DietPlanActions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
