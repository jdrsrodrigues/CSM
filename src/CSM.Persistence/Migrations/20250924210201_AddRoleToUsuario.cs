using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSM.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleToUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Perfil",
                table: "tbUsuario",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Perfil",
                table: "tbUsuario");
        }
    }
}
