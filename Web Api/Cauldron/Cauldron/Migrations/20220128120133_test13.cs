using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cauldron.Migrations
{
    public partial class test13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subs_AspNetUsers_UserId",
                table: "Subs");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Subs_UserId",
                table: "Subs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Subs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Subs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subs_UserId",
                table: "Subs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subs_AspNetUsers_UserId",
                table: "Subs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
