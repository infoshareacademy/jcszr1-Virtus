using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtusFitWeb.DAL.Migrations
{
    public partial class DailyDietPlanId_DietPlanId_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyDietPlans_DietPlans_DietPlanId",
                table: "DailyDietPlans");

            migrationBuilder.AlterColumn<int>(
                name: "DietPlanId",
                table: "DailyDietPlans",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyDietPlans_DietPlans_DietPlanId",
                table: "DailyDietPlans",
                column: "DietPlanId",
                principalTable: "DietPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyDietPlans_DietPlans_DietPlanId",
                table: "DailyDietPlans");

            migrationBuilder.AlterColumn<int>(
                name: "DietPlanId",
                table: "DailyDietPlans",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyDietPlans_DietPlans_DietPlanId",
                table: "DailyDietPlans",
                column: "DietPlanId",
                principalTable: "DietPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
