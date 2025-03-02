using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoStreamingPlatform.Database.Migrations
{
    /// <inheritdoc />
    public partial class addAltNameToEmojiShow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "altName",
                table: "EmojiShow",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "altName",
                table: "EmojiShow");
        }
    }
}
