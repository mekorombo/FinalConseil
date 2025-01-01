using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DashboardConseil.Migrations
{
    /// <inheritdoc />
    public partial class NewOffres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MinimumQualifications",
                table: "OffresEmploi",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreferredQualifications",
                table: "OffresEmploi",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumQualifications",
                table: "OffresEmploi");

            migrationBuilder.DropColumn(
                name: "PreferredQualifications",
                table: "OffresEmploi");
        }
    }
}
