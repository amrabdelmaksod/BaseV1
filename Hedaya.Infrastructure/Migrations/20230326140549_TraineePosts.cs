using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TraineePosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TraineeId",
                table: "Posts",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TraineeId",
                table: "Posts",
                column: "TraineeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Trainees_TraineeId",
                table: "Posts",
                column: "TraineeId",
                principalTable: "Trainees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Trainees_TraineeId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_TraineeId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "TraineeId",
                table: "Posts");
        }
    }
}
