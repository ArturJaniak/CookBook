using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyEmployees.Migrations
{
    public partial class test9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Ingredients_IngredientId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_IngredientId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Ingredient1",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ingredient10",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ingredient11",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ingredient12",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ingredient13",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ingredient14",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ingredient15",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ingredient16",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ingredient17",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ingredient18",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ingredient19",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ingredient2",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ingredient20",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ingredient3",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ingredient4",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ingredient5",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ingredient6",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ingredient7",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Ingredient8",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "Ingredient9",
                table: "Ingredients",
                newName: "Ingredient");

            migrationBuilder.AddColumn<Guid>(
                name: "RecipeId",
                table: "Ingredients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Subs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SubId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subs_AspNetUsers_SubId",
                        column: x => x.SubId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Subs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subs_SubId",
                table: "Subs",
                column: "SubId");

            migrationBuilder.CreateIndex(
                name: "IX_Subs_UserId",
                table: "Subs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Recipes_RecipeId",
                table: "Ingredients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipesId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Recipes_RecipeId",
                table: "Ingredients");

            migrationBuilder.DropTable(
                name: "Subs");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "Ingredient",
                table: "Ingredients",
                newName: "Ingredient9");

            migrationBuilder.AddColumn<Guid>(
                name: "IngredientId",
                table: "Recipes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Ingredient1",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient10",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient11",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient12",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient13",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient14",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient15",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient16",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient17",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient18",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient19",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient2",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient20",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient3",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient4",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient5",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient6",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient7",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient8",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_IngredientId",
                table: "Recipes",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Ingredients_IngredientId",
                table: "Recipes",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
