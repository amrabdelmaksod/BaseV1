using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExplanations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "MethodologicalExplanations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "MethodologicalExplanations",
                type: "DATETIME",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "MethodologicalExplanations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "MethodologicalExplanations",
                type: "DATETIME",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "MethodologicalExplanations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "MethodologicalExplanations");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "MethodologicalExplanations");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "MethodologicalExplanations");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "MethodologicalExplanations");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "MethodologicalExplanations");
        }
    }
}
