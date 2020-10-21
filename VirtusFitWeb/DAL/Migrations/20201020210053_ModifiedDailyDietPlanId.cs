using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtusFitWeb.DAL.Migrations
{
    public partial class ModifiedDailyDietPlanId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyDietPlan_DietPlans_DietPlanId1",
                table: "DailyDietPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInDietPlan_DailyDietPlan_DailyDietPlanDietPlanId",
                table: "ProductInDietPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInDietPlan_Products_ProductId",
                table: "ProductInDietPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInDietPlan",
                table: "ProductInDietPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyDietPlan",
                table: "DailyDietPlan");

            migrationBuilder.DropIndex(
                name: "IX_DailyDietPlan_DietPlanId1",
                table: "DailyDietPlan");

            migrationBuilder.DropColumn(
                name: "DietPlanId1",
                table: "DailyDietPlan");

            migrationBuilder.RenameTable(
                name: "ProductInDietPlan",
                newName: "ProductsInDietPlans");

            migrationBuilder.RenameTable(
                name: "DailyDietPlan",
                newName: "DailyDietPlans");

            migrationBuilder.RenameColumn(
                name: "DailyDietPlanDietPlanId",
                table: "ProductsInDietPlans",
                newName: "DailyDietPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInDietPlan_ProductId",
                table: "ProductsInDietPlans",
                newName: "IX_ProductsInDietPlans_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInDietPlan_DailyDietPlanDietPlanId",
                table: "ProductsInDietPlans",
                newName: "IX_ProductsInDietPlans_DailyDietPlanId");

            migrationBuilder.AddColumn<int>(
                name: "DietPlanId",
                table: "DailyDietPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsInDietPlans",
                table: "ProductsInDietPlans",
                column: "OrdinalNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyDietPlans",
                table: "DailyDietPlans",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DailyDietPlans_DietPlanId",
                table: "DailyDietPlans",
                column: "DietPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyDietPlans_DietPlans_DietPlanId",
                table: "DailyDietPlans",
                column: "DietPlanId",
                principalTable: "DietPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsInDietPlans_DailyDietPlans_DailyDietPlanId",
                table: "ProductsInDietPlans",
                column: "DailyDietPlanId",
                principalTable: "DailyDietPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsInDietPlans_Products_ProductId",
                table: "ProductsInDietPlans",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyDietPlans_DietPlans_DietPlanId",
                table: "DailyDietPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsInDietPlans_DailyDietPlans_DailyDietPlanId",
                table: "ProductsInDietPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsInDietPlans_Products_ProductId",
                table: "ProductsInDietPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsInDietPlans",
                table: "ProductsInDietPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyDietPlans",
                table: "DailyDietPlans");

            migrationBuilder.DropIndex(
                name: "IX_DailyDietPlans_DietPlanId",
                table: "DailyDietPlans");

            migrationBuilder.DropColumn(
                name: "DietPlanId",
                table: "DailyDietPlans");

            migrationBuilder.RenameTable(
                name: "ProductsInDietPlans",
                newName: "ProductInDietPlan");

            migrationBuilder.RenameTable(
                name: "DailyDietPlans",
                newName: "DailyDietPlan");

            migrationBuilder.RenameColumn(
                name: "DailyDietPlanId",
                table: "ProductInDietPlan",
                newName: "DailyDietPlanDietPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsInDietPlans_ProductId",
                table: "ProductInDietPlan",
                newName: "IX_ProductInDietPlan_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsInDietPlans_DailyDietPlanId",
                table: "ProductInDietPlan",
                newName: "IX_ProductInDietPlan_DailyDietPlanDietPlanId");

            migrationBuilder.AddColumn<int>(
                name: "DietPlanId1",
                table: "DailyDietPlan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInDietPlan",
                table: "ProductInDietPlan",
                column: "OrdinalNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyDietPlan",
                table: "DailyDietPlan",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DailyDietPlan_DietPlanId1",
                table: "DailyDietPlan",
                column: "DietPlanId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyDietPlan_DietPlans_DietPlanId1",
                table: "DailyDietPlan",
                column: "DietPlanId1",
                principalTable: "DietPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInDietPlan_DailyDietPlan_DailyDietPlanDietPlanId",
                table: "ProductInDietPlan",
                column: "DailyDietPlanDietPlanId",
                principalTable: "DailyDietPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInDietPlan_Products_ProductId",
                table: "ProductInDietPlan",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
