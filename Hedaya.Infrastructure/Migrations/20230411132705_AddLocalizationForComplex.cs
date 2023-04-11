using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLocalizationForComplex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Vision",
                table: "Complexes",
                newName: "VisionEn");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Complexes",
                newName: "TitleAr");

            migrationBuilder.RenameColumn(
                name: "Terms",
                table: "Complexes",
                newName: "VisionAr");

            migrationBuilder.RenameColumn(
                name: "Mission",
                table: "Complexes",
                newName: "TermsEn");

            migrationBuilder.RenameColumn(
                name: "LogFiles",
                table: "Complexes",
                newName: "TermsAr");

            migrationBuilder.RenameColumn(
                name: "Cookies",
                table: "Complexes",
                newName: "MissionEn");

            migrationBuilder.RenameColumn(
                name: "Conditions",
                table: "Complexes",
                newName: "MissionAr");

            migrationBuilder.AddColumn<string>(
                name: "ConditionsAr",
                table: "Complexes",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ConditionsEn",
                table: "Complexes",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CookiesAr",
                table: "Complexes",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CookiesEn",
                table: "Complexes",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LogFilesAr",
                table: "Complexes",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LogFilesEn",
                table: "Complexes",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleEn",
                table: "Complexes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConditionsAr",
                table: "Complexes");

            migrationBuilder.DropColumn(
                name: "ConditionsEn",
                table: "Complexes");

            migrationBuilder.DropColumn(
                name: "CookiesAr",
                table: "Complexes");

            migrationBuilder.DropColumn(
                name: "CookiesEn",
                table: "Complexes");

            migrationBuilder.DropColumn(
                name: "LogFilesAr",
                table: "Complexes");

            migrationBuilder.DropColumn(
                name: "LogFilesEn",
                table: "Complexes");

            migrationBuilder.DropColumn(
                name: "TitleEn",
                table: "Complexes");

            migrationBuilder.RenameColumn(
                name: "VisionEn",
                table: "Complexes",
                newName: "Vision");

            migrationBuilder.RenameColumn(
                name: "VisionAr",
                table: "Complexes",
                newName: "Terms");

            migrationBuilder.RenameColumn(
                name: "TitleAr",
                table: "Complexes",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "TermsEn",
                table: "Complexes",
                newName: "Mission");

            migrationBuilder.RenameColumn(
                name: "TermsAr",
                table: "Complexes",
                newName: "LogFiles");

            migrationBuilder.RenameColumn(
                name: "MissionEn",
                table: "Complexes",
                newName: "Cookies");

            migrationBuilder.RenameColumn(
                name: "MissionAr",
                table: "Complexes",
                newName: "Conditions");
        }
    }
}
