using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TraineeCourseFavorites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TraineeCourseFavorite_Courses_CourseId",
                table: "TraineeCourseFavorite");

            migrationBuilder.DropForeignKey(
                name: "FK_TraineeCourseFavorite_Trainees_TraineeId",
                table: "TraineeCourseFavorite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TraineeCourseFavorite",
                table: "TraineeCourseFavorite");

            migrationBuilder.RenameTable(
                name: "TraineeCourseFavorite",
                newName: "TraineeCourseFavorites");

            migrationBuilder.RenameIndex(
                name: "IX_TraineeCourseFavorite_CourseId",
                table: "TraineeCourseFavorites",
                newName: "IX_TraineeCourseFavorites_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TraineeCourseFavorites",
                table: "TraineeCourseFavorites",
                columns: new[] { "TraineeId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeCourseFavorites_Courses_CourseId",
                table: "TraineeCourseFavorites",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeCourseFavorites_Trainees_TraineeId",
                table: "TraineeCourseFavorites",
                column: "TraineeId",
                principalTable: "Trainees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TraineeCourseFavorites_Courses_CourseId",
                table: "TraineeCourseFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_TraineeCourseFavorites_Trainees_TraineeId",
                table: "TraineeCourseFavorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TraineeCourseFavorites",
                table: "TraineeCourseFavorites");

            migrationBuilder.RenameTable(
                name: "TraineeCourseFavorites",
                newName: "TraineeCourseFavorite");

            migrationBuilder.RenameIndex(
                name: "IX_TraineeCourseFavorites_CourseId",
                table: "TraineeCourseFavorite",
                newName: "IX_TraineeCourseFavorite_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TraineeCourseFavorite",
                table: "TraineeCourseFavorite",
                columns: new[] { "TraineeId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeCourseFavorite_Courses_CourseId",
                table: "TraineeCourseFavorite",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeCourseFavorite_Trainees_TraineeId",
                table: "TraineeCourseFavorite",
                column: "TraineeId",
                principalTable: "Trainees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
