using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EnrollInProgramUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Trainees_TraineeId1",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_TrainingPrograms_TrainingProgramId",
                table: "Enrollment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollment",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_TraineeId1",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "TraineeId1",
                table: "Enrollment");

            migrationBuilder.RenameTable(
                name: "Enrollment",
                newName: "Enrollments");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollment_TrainingProgramId_Email",
                table: "Enrollments",
                newName: "IX_Enrollments_TrainingProgramId_Email");

            migrationBuilder.AlterColumn<string>(
                name: "TraineeId",
                table: "Enrollments",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_TraineeId",
                table: "Enrollments",
                column: "TraineeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Trainees_TraineeId",
                table: "Enrollments",
                column: "TraineeId",
                principalTable: "Trainees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_TrainingPrograms_TrainingProgramId",
                table: "Enrollments",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Trainees_TraineeId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_TrainingPrograms_TrainingProgramId",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_TraineeId",
                table: "Enrollments");

            migrationBuilder.RenameTable(
                name: "Enrollments",
                newName: "Enrollment");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_TrainingProgramId_Email",
                table: "Enrollment",
                newName: "IX_Enrollment_TrainingProgramId_Email");

            migrationBuilder.AlterColumn<int>(
                name: "TraineeId",
                table: "Enrollment",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AddColumn<string>(
                name: "TraineeId1",
                table: "Enrollment",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollment",
                table: "Enrollment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_TraineeId1",
                table: "Enrollment",
                column: "TraineeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Trainees_TraineeId1",
                table: "Enrollment",
                column: "TraineeId1",
                principalTable: "Trainees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_TrainingPrograms_TrainingProgramId",
                table: "Enrollment",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
