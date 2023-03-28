using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ImplementTraineeCourseFavourites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TraineeCourseFavorite",
                columns: table => new
                {
                    TraineeId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraineeCourseFavorite", x => new { x.TraineeId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_TraineeCourseFavorite_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TraineeCourseFavorite_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Trainees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TraineeCourseFavorite_CourseId",
                table: "TraineeCourseFavorite",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TraineeCourseFavorite");
        }
    }
}
