using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseV1.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class revertToInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Testssss",
                table: "Testssss");

            migrationBuilder.RenameTable(
                name: "Testssss",
                newName: "Tests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tests",
                table: "Tests",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tests",
                table: "Tests");

            migrationBuilder.RenameTable(
                name: "Tests",
                newName: "Testssss");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Testssss",
                table: "Testssss",
                column: "Id");
        }
    }
}
