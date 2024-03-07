using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projekt_webbservice.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAudioModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoFileName",
                table: "Audio",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoFileName",
                table: "Audio");
        }
    }
}
