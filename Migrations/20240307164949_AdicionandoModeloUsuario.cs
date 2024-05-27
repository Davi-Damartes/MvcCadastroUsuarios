using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcCadastro.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoModeloUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Usuarios");
        }
    }
}
