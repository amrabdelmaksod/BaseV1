using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProgramNoteWithProgramId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingProgramNotes_TrainingPrograms_TrainingProgramId",
                table: "TrainingProgramNotes");

            migrationBuilder.AlterColumn<int>(
                name: "TrainingProgramId",
                table: "TrainingProgramNotes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingProgramNotes_TrainingPrograms_TrainingProgramId",
                table: "TrainingProgramNotes",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingProgramNotes_TrainingPrograms_TrainingProgramId",
                table: "TrainingProgramNotes");

            migrationBuilder.AlterColumn<int>(
                name: "TrainingProgramId",
                table: "TrainingProgramNotes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingProgramNotes_TrainingPrograms_TrainingProgramId",
                table: "TrainingProgramNotes",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id");
        }
    }
}
