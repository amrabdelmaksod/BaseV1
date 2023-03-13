using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addPlatformAxisFeaturesFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Forums");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Forums");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Forums");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "EducationalCourses");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "EducationalCourses");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "EducationalCourses");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "CourseTopics");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "CourseTopics");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "CourseTopics");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Blogs");

            migrationBuilder.CreateTable(
                name: "PlatformFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValue: new DateTime(2023, 3, 7, 14, 26, 59, 661, DateTimeKind.Local).AddTicks(3048)),
                    ModifiedById = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlatformFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValue: new DateTime(2023, 3, 7, 14, 26, 59, 661, DateTimeKind.Local).AddTicks(5277)),
                    ModifiedById = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlatformWorkAxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValue: new DateTime(2023, 3, 7, 14, 26, 59, 661, DateTimeKind.Local).AddTicks(7444)),
                    ModifiedById = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformWorkAxes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlatformFeatures");

            migrationBuilder.DropTable(
                name: "PlatformFields");

            migrationBuilder.DropTable(
                name: "PlatformWorkAxes");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Replies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Replies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Replies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Lessons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Lessons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Lessons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Forums",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Forums",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Forums",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "EducationalCourses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "EducationalCourses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "EducationalCourses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "CourseTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "CourseTopics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "CourseTopics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
