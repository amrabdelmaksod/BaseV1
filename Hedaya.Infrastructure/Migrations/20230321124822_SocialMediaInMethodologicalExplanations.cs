using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SocialMediaInMethodologicalExplanations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "MethodologicalExplanations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telegram",
                table: "MethodologicalExplanations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "MethodologicalExplanations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Whatsapp",
                table: "MethodologicalExplanations",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Youtube",
                table: "MethodologicalExplanations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "MethodologicalExplanations");

            migrationBuilder.DropColumn(
                name: "Telegram",
                table: "MethodologicalExplanations");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "MethodologicalExplanations");

            migrationBuilder.DropColumn(
                name: "Whatsapp",
                table: "MethodologicalExplanations");

            migrationBuilder.DropColumn(
                name: "Youtube",
                table: "MethodologicalExplanations");
        }
    }
}
