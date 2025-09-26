using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSM.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveChatTokenToUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelegramChatToken",
                table: "tbUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TelegramChatToken",
                table: "tbUsuario",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
