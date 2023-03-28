using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addForumToCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Forums_CourseId",
                table: "Forums");

            migrationBuilder.AddColumn<int>(
                name: "ForumId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Forums_CourseId",
                table: "Forums",
                column: "CourseId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Forums_CourseId",
                table: "Forums");

            migrationBuilder.DropColumn(
                name: "ForumId",
                table: "Courses");

            migrationBuilder.CreateIndex(
                name: "IX_Forums_CourseId",
                table: "Forums",
                column: "CourseId");
        }
    }
}
