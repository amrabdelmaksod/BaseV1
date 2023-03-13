using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatesOnTraineeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EducationalDegree",
                table: "Trainees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Trainees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Trainees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "Trainees",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telegram",
                table: "Trainees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "Trainees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Whatsapp",
                table: "Trainees",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1997, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "AppUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EducationalDegree",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "Telegram",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "Whatsapp",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AppUsers");
        }
    }
}
