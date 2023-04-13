using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CommonQuestionsBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "CommonQuestions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "CommonQuestions",
                type: "DATETIME",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "CommonQuestions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "CommonQuestions",
                type: "DATETIME",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "CommonQuestions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "CommonQuestions");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "CommonQuestions");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "CommonQuestions");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "CommonQuestions");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "CommonQuestions");
        }
    }
}
