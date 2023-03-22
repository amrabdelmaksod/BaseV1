using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExplanationNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MethodologicalExplanations_SubCategories_SubCategoryId",
                table: "MethodologicalExplanations");

            migrationBuilder.AddColumn<string>(
                name: "InstructorId",
                table: "MethodologicalExplanations",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ExplanationNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    MethodologicalExplanationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExplanationNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExplanationNotes_MethodologicalExplanations_MethodologicalExplanationId",
                        column: x => x.MethodologicalExplanationId,
                        principalTable: "MethodologicalExplanations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

      

            migrationBuilder.CreateIndex(
                name: "IX_ExplanationNotes_MethodologicalExplanationId",
                table: "ExplanationNotes",
                column: "MethodologicalExplanationId");

      

            migrationBuilder.AddForeignKey(
                name: "FK_MethodologicalExplanations_SubCategories_SubCategoryId",
                table: "MethodologicalExplanations",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropForeignKey(
                name: "FK_MethodologicalExplanations_SubCategories_SubCategoryId",
                table: "MethodologicalExplanations");

            migrationBuilder.DropTable(
                name: "ExplanationNotes");

         

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "MethodologicalExplanations");

            migrationBuilder.AddForeignKey(
                name: "FK_MethodologicalExplanations_SubCategories_SubCategoryId",
                table: "MethodologicalExplanations",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
