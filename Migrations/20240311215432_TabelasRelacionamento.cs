using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcCadastro.Migrations
{
    /// <inheritdoc />
    public partial class TabelasRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contatos_Usuarios_UsuarioId",
                table: "Contatos");

            migrationBuilder.DropIndex(
                name: "IX_Contatos_UsuarioId",
                table: "Contatos");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Contatos",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId1",
                table: "Contatos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contatos_UsuarioId1",
                table: "Contatos",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Contatos_Usuarios_UsuarioId1",
                table: "Contatos",
                column: "UsuarioId1",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contatos_Usuarios_UsuarioId1",
                table: "Contatos");

            migrationBuilder.DropIndex(
                name: "IX_Contatos_UsuarioId1",
                table: "Contatos");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Contatos");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Contatos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contatos_UsuarioId",
                table: "Contatos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contatos_Usuarios_UsuarioId",
                table: "Contatos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
