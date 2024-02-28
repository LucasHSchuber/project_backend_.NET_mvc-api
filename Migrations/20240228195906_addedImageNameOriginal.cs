using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projekt_webbservice.Migrations
{
    /// <inheritdoc />
    public partial class addedImageNameOriginal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageNameOriginal",
                table: "Audio",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageNameOriginal",
                table: "Audio");
        }
    }
}
