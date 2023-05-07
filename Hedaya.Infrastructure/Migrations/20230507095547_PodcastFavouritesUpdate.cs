using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PodcastFavouritesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PodcastFavourite_Podcasts_PodcastId",
                table: "PodcastFavourite");

            migrationBuilder.DropForeignKey(
                name: "FK_PodcastFavourite_Trainees_TraineeId",
                table: "PodcastFavourite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PodcastFavourite",
                table: "PodcastFavourite");

            migrationBuilder.RenameTable(
                name: "PodcastFavourite",
                newName: "PodcastFavourites");

            migrationBuilder.RenameIndex(
                name: "IX_PodcastFavourite_PodcastId",
                table: "PodcastFavourites",
                newName: "IX_PodcastFavourites_PodcastId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PodcastFavourites",
                table: "PodcastFavourites",
                columns: new[] { "TraineeId", "PodcastId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PodcastFavourites_Podcasts_PodcastId",
                table: "PodcastFavourites",
                column: "PodcastId",
                principalTable: "Podcasts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PodcastFavourites_Trainees_TraineeId",
                table: "PodcastFavourites",
                column: "TraineeId",
                principalTable: "Trainees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PodcastFavourites_Podcasts_PodcastId",
                table: "PodcastFavourites");

            migrationBuilder.DropForeignKey(
                name: "FK_PodcastFavourites_Trainees_TraineeId",
                table: "PodcastFavourites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PodcastFavourites",
                table: "PodcastFavourites");

            migrationBuilder.RenameTable(
                name: "PodcastFavourites",
                newName: "PodcastFavourite");

            migrationBuilder.RenameIndex(
                name: "IX_PodcastFavourites_PodcastId",
                table: "PodcastFavourite",
                newName: "IX_PodcastFavourite_PodcastId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PodcastFavourite",
                table: "PodcastFavourite",
                columns: new[] { "TraineeId", "PodcastId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PodcastFavourite_Podcasts_PodcastId",
                table: "PodcastFavourite",
                column: "PodcastId",
                principalTable: "Podcasts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PodcastFavourite_Trainees_TraineeId",
                table: "PodcastFavourite",
                column: "TraineeId",
                principalTable: "Trainees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
