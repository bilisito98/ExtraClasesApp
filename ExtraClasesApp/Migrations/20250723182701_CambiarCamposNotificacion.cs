using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtraClasesApp.Migrations
{
    /// <inheritdoc />
    public partial class CambiarCamposNotificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contenido",
                table: "Notificaciones");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Notificaciones",
                newName: "Mensaje");

            migrationBuilder.AddColumn<string>(
                name: "Asunto",
                table: "Notificaciones",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Asunto",
                table: "Notificaciones");

            migrationBuilder.RenameColumn(
                name: "Mensaje",
                table: "Notificaciones",
                newName: "Tipo");

            migrationBuilder.AddColumn<string>(
                name: "Contenido",
                table: "Notificaciones",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
