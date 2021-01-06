using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevsHub.Data.Migrations
{
    public partial class TutorialCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TutorialTutorialCategory",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TutorialsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorialTutorialCategory", x => new { x.CategoriesId, x.TutorialsId });
                    table.ForeignKey(
                        name: "FK_TutorialTutorialCategory_TutorialCategories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "TutorialCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorialTutorialCategory_Tutorials_TutorialsId",
                        column: x => x.TutorialsId,
                        principalTable: "Tutorials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TutorialTutorialCategory_TutorialsId",
                table: "TutorialTutorialCategory",
                column: "TutorialsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutorialTutorialCategory");
        }
    }
}
