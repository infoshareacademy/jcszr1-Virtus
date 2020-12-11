using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtusFitApi.Migrations
{
    public partial class PlanActionsUpdateNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DietPlanId",
                table: "DietPlanActions",
                newName: "DietPlans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DietPlans",
                table: "DietPlanActions",
                newName: "DietPlanId");
        }
    }
}
