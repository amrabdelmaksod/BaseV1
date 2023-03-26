using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class coursesDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Lessons",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CourseTopics",
                newName: "NameEn");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Lessons",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SortIndex",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "CourseTopics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SortIndex",
                table: "CourseTopics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AboutCourse",
                table: "Courses",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CourseFeatures",
                table: "Courses",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CourseSyllabus",
                table: "Courses",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "SortIndex",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "CourseTopics");

            migrationBuilder.DropColumn(
                name: "SortIndex",
                table: "CourseTopics");

            migrationBuilder.DropColumn(
                name: "AboutCourse",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseFeatures",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseSyllabus",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Lessons",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "CourseTopics",
                newName: "Name");
        }
    }
}
