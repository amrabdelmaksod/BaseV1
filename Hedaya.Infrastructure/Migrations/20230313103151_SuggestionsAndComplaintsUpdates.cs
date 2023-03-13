using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SuggestionsAndComplaintsUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SuggestionAndComplaint",
                table: "SuggestionAndComplaint");

            migrationBuilder.RenameTable(
                name: "SuggestionAndComplaint",
                newName: "SuggestionAndComplaints");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SuggestionAndComplaints",
                table: "SuggestionAndComplaints",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SuggestionAndComplaints",
                table: "SuggestionAndComplaints");

            migrationBuilder.RenameTable(
                name: "SuggestionAndComplaints",
                newName: "SuggestionAndComplaint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SuggestionAndComplaint",
                table: "SuggestionAndComplaint",
                column: "Id");
        }
    }
}
