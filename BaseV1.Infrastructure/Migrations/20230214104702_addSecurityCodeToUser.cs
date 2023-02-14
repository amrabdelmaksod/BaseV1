using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseV1.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addSecurityCodeToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecurityCode",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecurityCode",
                table: "AppUsers");
        }
    }
}
