using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtusFitApi.Migrations
{
    public partial class RemoveUnseEntitis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyDietPlanActions");

            migrationBuilder.AddColumn<int>(
                name: "CaloriesPerDay",
                table: "DietPlanActions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaloriesPerDay",
                table: "DietPlanActions");

            migrationBuilder.CreateTable(
                name: "DailyDietPlanActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DailyDietPlanId = table.Column<int>(type: "int", nullable: false),
                    DietPlanId = table.Column<int>(type: "int", nullable: false),
                    TotalCalories = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyDietPlanActions", x => x.Id);
                });
        }
    }
}
