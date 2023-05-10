using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TraineeLessonsUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "TraineeLessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TraineeLessons_CourseId",
                table: "TraineeLessons",
                column: "CourseId");

          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.DropIndex(
                name: "IX_TraineeLessons_CourseId",
                table: "TraineeLessons");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "TraineeLessons");
        }
    }
}
