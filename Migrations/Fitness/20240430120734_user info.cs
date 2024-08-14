using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations.Fitness
{
    /// <inheritdoc />
    public partial class userinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "FoodUnits",
                newName: "FoodUnitId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Foods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "NutritionPlans",
                columns: table => new
                {
                    NutritionPlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionPlans", x => x.NutritionPlanId);
                });

            migrationBuilder.CreateTable(
                name: "NutritionPlanMeals",
                columns: table => new
                {
                    NutritionPlanMealId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NutritionPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionPlanMeals", x => x.NutritionPlanMealId);
                    table.ForeignKey(
                        name: "FK_NutritionPlanMeals_NutritionPlans_NutritionPlanId",
                        column: x => x.NutritionPlanId,
                        principalTable: "NutritionPlans",
                        principalColumn: "NutritionPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NutritionPlanFoods",
                columns: table => new
                {
                    NutritionPlanFoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Eaten = table.Column<bool>(type: "bit", nullable: false),
                    NutritionPlanMealId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionPlanFoods", x => x.NutritionPlanFoodId);
                    table.ForeignKey(
                        name: "FK_NutritionPlanFoods_NutritionPlanMeals_NutritionPlanMealId",
                        column: x => x.NutritionPlanMealId,
                        principalTable: "NutritionPlanMeals",
                        principalColumn: "NutritionPlanMealId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NutritionPlanFoods_NutritionPlanMealId",
                table: "NutritionPlanFoods",
                column: "NutritionPlanMealId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionPlanMeals_NutritionPlanId",
                table: "NutritionPlanMeals",
                column: "NutritionPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NutritionPlanFoods");

            migrationBuilder.DropTable(
                name: "NutritionPlanMeals");

            migrationBuilder.DropTable(
                name: "NutritionPlans");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Foods");

            migrationBuilder.RenameColumn(
                name: "FoodUnitId",
                table: "FoodUnits",
                newName: "Id");
        }
    }
}
