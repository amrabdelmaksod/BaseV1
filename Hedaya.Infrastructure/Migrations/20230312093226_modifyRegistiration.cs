using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifyRegistiration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AppUsers");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Trainees",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cookies",
                table: "Complexes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LogFiles",
                table: "Complexes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Nationality",
                table: "AppUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "Cookies",
                table: "Complexes");

            migrationBuilder.DropColumn(
                name: "LogFiles",
                table: "Complexes");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "AppUsers");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Trainees",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Trainees",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AppUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
