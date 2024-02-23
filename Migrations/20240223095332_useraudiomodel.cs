using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projekt_webbservice.Migrations
{
    /// <inheritdoc />
    public partial class useraudiomodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAudio",
                columns: table => new
                {
                    UserAudioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AudioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAudio", x => x.UserAudioId);
                    table.ForeignKey(
                        name: "FK_UserAudio_Audio_AudioId",
                        column: x => x.AudioId,
                        principalTable: "Audio",
                        principalColumn: "AudioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAudio_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAudio_AudioId",
                table: "UserAudio",
                column: "AudioId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAudio_UserId",
                table: "UserAudio",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAudio");
        }
    }
}
