using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cauldron.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergens",
                columns: table => new
                {
                    AllergenId = table.Column<Guid>(nullable: false),
                    Alergen = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergens", x => x.AllergenId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<Guid>(nullable: false),
                    Ingredient1 = table.Column<string>(nullable: true),
                    Ingredient2 = table.Column<string>(nullable: true),
                    Ingredient3 = table.Column<string>(nullable: true),
                    Ingredient4 = table.Column<string>(nullable: true),
                    Ingredient5 = table.Column<string>(nullable: true),
                    Ingredient6 = table.Column<string>(nullable: true),
                    Ingredient7 = table.Column<string>(nullable: true),
                    Ingredient8 = table.Column<string>(nullable: true),
                    Ingredient9 = table.Column<string>(nullable: true),
                    Ingredient10 = table.Column<string>(nullable: true),
                    Ingredient11 = table.Column<string>(nullable: true),
                    Ingredient12 = table.Column<string>(nullable: true),
                    Ingredient13 = table.Column<string>(nullable: true),
                    Ingredient14 = table.Column<string>(nullable: true),
                    Ingredient15 = table.Column<string>(nullable: true),
                    Ingredient16 = table.Column<string>(nullable: true),
                    Ingredient17 = table.Column<string>(nullable: true),
                    Ingredient18 = table.Column<string>(nullable: true),
                    Ingredient19 = table.Column<string>(nullable: true),
                    Ingredient20 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingId = table.Column<Guid>(nullable: false),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<Guid>(nullable: false),
                    Vegan = table.Column<bool>(nullable: false),
                    Vege = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipesId = table.Column<Guid>(nullable: false),
                    Photo = table.Column<string>(nullable: true),
                    Instruction = table.Column<string>(nullable: true),
                    IfPublic = table.Column<bool>(nullable: false),
                    RatingCounter = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    AllergenId = table.Column<Guid>(nullable: false),
                    IngredientId = table.Column<Guid>(nullable: false),
                    RatingId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipesId);
                    table.ForeignKey(
                        name: "FK_Recipes_Allergens_AllergenId",
                        column: x => x.AllergenId,
                        principalTable: "Allergens",
                        principalColumn: "AllergenId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipes_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipes_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "RatingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipes_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeList",
                columns: table => new
                {
                    RecipeListId = table.Column<Guid>(nullable: false),
                    RecipeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeList", x => x.RecipeListId);
                    table.ForeignKey(
                        name: "FK_RecipeList_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeList_RecipeId",
                table: "RecipeList",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_AllergenId",
                table: "Recipes",
                column: "AllergenId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_IngredientId",
                table: "Recipes",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_RatingId",
                table: "Recipes",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_TagId",
                table: "Recipes",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeList");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Allergens");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
