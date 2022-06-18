using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cauldron.Migrations
{
    public partial class Test16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageList");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageList",
                columns: table => new
                {
                    ImageListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageList", x => x.ImageListId);
                    table.ForeignKey(
                        name: "FK_ImageList_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageList_RecipeId",
                table: "ImageList",
                column: "RecipeId");
        }
    }
}
