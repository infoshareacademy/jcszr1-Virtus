using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtusFitApi.Migrations
{
    public partial class BmiActionUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "BmiActions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "BmiActions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
