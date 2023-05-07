using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueIndexFromProgramEnrollments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Enrollments_TrainingProgramId_Email",
                table: "Enrollments");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_TrainingProgramId",
                table: "Enrollments",
                column: "TrainingProgramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Enrollments_TrainingProgramId",
                table: "Enrollments");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_TrainingProgramId_Email",
                table: "Enrollments",
                columns: new[] { "TrainingProgramId", "Email" },
                unique: true);
        }
    }
}
