using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSM.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbUsuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    TelegramChatId = table.Column<string>(type: "TEXT", nullable: false),
                    TelegramChatToken = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbAlerta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Simbolo = table.Column<string>(type: "TEXT", nullable: false),
                    PrecoAlvo = table.Column<decimal>(type: "TEXT", nullable: false),
                    NivelVolume = table.Column<decimal>(type: "TEXT", nullable: false),
                    NivelMacd = table.Column<decimal>(type: "TEXT", nullable: false),
                    NivelRsi = table.Column<decimal>(type: "TEXT", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbAlerta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbAlerta_tbUsuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "tbUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbAlerta_IdUsuario",
                table: "tbAlerta",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbAlerta");

            migrationBuilder.DropTable(
                name: "tbUsuario");
        }
    }
}
