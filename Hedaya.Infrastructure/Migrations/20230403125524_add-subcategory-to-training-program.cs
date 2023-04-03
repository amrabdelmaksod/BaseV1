using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addsubcategorytotrainingprogram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "TrainingPrograms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_SubCategoryId",
                table: "TrainingPrograms",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPrograms_SubCategories_SubCategoryId",
                table: "TrainingPrograms",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_SubCategories_SubCategoryId",
                table: "TrainingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPrograms_SubCategoryId",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "TrainingPrograms");
        }
    }
}
