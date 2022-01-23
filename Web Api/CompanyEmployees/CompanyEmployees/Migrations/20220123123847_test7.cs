using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyEmployees.Migrations
{
    public partial class test7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Ratings_RatingId",
                table: "Recipes");

            //migrationBuilder.DropTable(
            //    name: "Test3");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_RatingId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RatingCounter",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RatingId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Alergen",
                table: "Allergens");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Recipes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RecipesId",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Ratings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CELERY",
                table: "Allergens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EGGS",
                table: "Allergens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FISH",
                table: "Allergens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GLUTEN",
                table: "Allergens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LUPINE",
                table: "Allergens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Lactose",
                table: "Allergens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MUSCLES",
                table: "Allergens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MUSTARD",
                table: "Allergens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PEANUTS",
                table: "Allergens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SESAME",
                table: "Allergens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SHELLFISH",
                table: "Allergens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SOY",
                table: "Allergens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SULPHUR_DIOXIDE",
                table: "Allergens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RecipesId",
                table: "Ratings",
                column: "RecipesId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId",
                table: "Ratings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Recipes_RecipesId",
                table: "Ratings",
                column: "RecipesId",
                principalTable: "Recipes",
                principalColumn: "RecipesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_UserId",
                table: "Recipes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Recipes_RecipesId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_UserId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_RecipesId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RecipesId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "CELERY",
                table: "Allergens");

            migrationBuilder.DropColumn(
                name: "EGGS",
                table: "Allergens");

            migrationBuilder.DropColumn(
                name: "FISH",
                table: "Allergens");

            migrationBuilder.DropColumn(
                name: "GLUTEN",
                table: "Allergens");

            migrationBuilder.DropColumn(
                name: "LUPINE",
                table: "Allergens");

            migrationBuilder.DropColumn(
                name: "Lactose",
                table: "Allergens");

            migrationBuilder.DropColumn(
                name: "MUSCLES",
                table: "Allergens");

            migrationBuilder.DropColumn(
                name: "MUSTARD",
                table: "Allergens");

            migrationBuilder.DropColumn(
                name: "PEANUTS",
                table: "Allergens");

            migrationBuilder.DropColumn(
                name: "SESAME",
                table: "Allergens");

            migrationBuilder.DropColumn(
                name: "SHELLFISH",
                table: "Allergens");

            migrationBuilder.DropColumn(
                name: "SOY",
                table: "Allergens");

            migrationBuilder.DropColumn(
                name: "SULPHUR_DIOXIDE",
                table: "Allergens");

            migrationBuilder.AddColumn<int>(
                name: "RatingCounter",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "RatingId",
                table: "Recipes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Alergen",
                table: "Allergens",
                type: "nvarchar(max)",
                nullable: true);

            //migrationBuilder.CreateTable(
            //    name: "Test3",
            //    columns: table => new
            //    {
            //        test3Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        X = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Y = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Z = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Test3", x => x.test3Id);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_RatingId",
                table: "Recipes",
                column: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Ratings_RatingId",
                table: "Recipes",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "RatingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
