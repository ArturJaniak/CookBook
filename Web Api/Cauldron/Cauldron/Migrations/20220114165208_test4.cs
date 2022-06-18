using Microsoft.EntityFrameworkCore.Migrations;

namespace Cauldron.Migrations
{
    public partial class test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "RecipeList",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeList_UserId",
                table: "RecipeList",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeList_AspNetUsers_UserId",
                table: "RecipeList",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeList_AspNetUsers_UserId",
                table: "RecipeList");

            migrationBuilder.DropIndex(
                name: "IX_RecipeList_UserId",
                table: "RecipeList");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RecipeList");
        }
    }
}
