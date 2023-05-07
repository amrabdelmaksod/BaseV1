using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExplanationFavourites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TraineeExplanationFavourites",
                columns: table => new
                {
                    TraineeId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    MethodologicalExplanationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraineeExplanationFavourites", x => new { x.TraineeId, x.MethodologicalExplanationId });
                    table.ForeignKey(
                        name: "FK_TraineeExplanationFavourites_MethodologicalExplanations_MethodologicalExplanationId",
                        column: x => x.MethodologicalExplanationId,
                        principalTable: "MethodologicalExplanations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TraineeExplanationFavourites_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Trainees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TraineeExplanationFavourites_MethodologicalExplanationId",
                table: "TraineeExplanationFavourites",
                column: "MethodologicalExplanationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TraineeExplanationFavourites");
        }
    }
}
