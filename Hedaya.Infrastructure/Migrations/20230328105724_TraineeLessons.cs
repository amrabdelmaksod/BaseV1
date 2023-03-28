using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TraineeLessons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TraineeLessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraineeId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraineeLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TraineeLessons_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TraineeLessons_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Trainees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TraineeLessons_LessonId",
                table: "TraineeLessons",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_TraineeLessons_TraineeId",
                table: "TraineeLessons",
                column: "TraineeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TraineeLessons");
        }
    }
}
