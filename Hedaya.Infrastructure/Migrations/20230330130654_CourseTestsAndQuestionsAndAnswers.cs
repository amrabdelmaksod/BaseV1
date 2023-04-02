using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CourseTestsAndQuestionsAndAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CourseTests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseTests_CourseId",
                table: "CourseTests",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTests_Courses_CourseId",
                table: "CourseTests",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTests_Courses_CourseId",
                table: "CourseTests");

            migrationBuilder.DropIndex(
                name: "IX_CourseTests_CourseId",
                table: "CourseTests");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseTests");
        }
    }
}
