using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projekt_webbservice.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAudioModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "VideoData",
                table: "Audio",
                type: "BLOB",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoData",
                table: "Audio");
        }
    }
}
