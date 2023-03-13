using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hedaya.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifyCreationDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "PlatformWorkAxes",
                type: "DATETIME",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2023, 3, 7, 14, 26, 59, 661, DateTimeKind.Local).AddTicks(7444));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "PlatformFields",
                type: "DATETIME",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2023, 3, 7, 14, 26, 59, 661, DateTimeKind.Local).AddTicks(5277));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "PlatformFeatures",
                type: "DATETIME",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValue: new DateTime(2023, 3, 7, 14, 26, 59, 661, DateTimeKind.Local).AddTicks(3048));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "PlatformWorkAxes",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 7, 14, 26, 59, 661, DateTimeKind.Local).AddTicks(7444),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "PlatformFields",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 7, 14, 26, 59, 661, DateTimeKind.Local).AddTicks(5277),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "PlatformFeatures",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 7, 14, 26, 59, 661, DateTimeKind.Local).AddTicks(3048),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
