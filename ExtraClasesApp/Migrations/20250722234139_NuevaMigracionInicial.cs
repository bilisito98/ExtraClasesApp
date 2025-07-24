using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExtraClasesApp.Migrations
{
    /// <inheritdoc />
    public partial class NuevaMigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tutores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreCompleto = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: true),
                    Pais = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreCompleto = table.Column<string>(type: "text", nullable: false),
                    EmailPadre = table.Column<string>(type: "text", nullable: false),
                    TelefonoPadre = table.Column<string>(type: "text", nullable: false),
                    Pais = table.Column<string>(type: "text", nullable: false),
                    Curso = table.Column<string>(type: "text", nullable: false),
                    TutorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estudiantes_Tutores_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TutorId = table.Column<int>(type: "integer", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Destinatario = table.Column<string>(type: "text", nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Contenido = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Tutores_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreferenciasTutores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TutorId = table.Column<int>(type: "integer", nullable: false),
                    RecibirNotificacionesSMS = table.Column<bool>(type: "boolean", nullable: false),
                    RecibirNotificacionesEmail = table.Column<bool>(type: "boolean", nullable: false),
                    HoraResumenDiario = table.Column<TimeSpan>(type: "interval", nullable: false),
                    ZonaHoraria = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenciasTutores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreferenciasTutores_Tutores_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClasesExtras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaHoraTutor = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaHoraEstudiante = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modulo = table.Column<string>(type: "text", nullable: false),
                    Leccion = table.Column<string>(type: "text", nullable: false),
                    EstudianteId = table.Column<int>(type: "integer", nullable: false),
                    TutorId = table.Column<int>(type: "integer", nullable: false),
                    Notificado = table.Column<bool>(type: "boolean", nullable: false),
                    ModuloId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasesExtras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClasesExtras_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClasesExtras_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClasesExtras_Tutores_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MensajesEnviados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Destinatario = table.Column<string>(type: "text", nullable: false),
                    Contenido = table.Column<string>(type: "text", nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TutorId = table.Column<int>(type: "integer", nullable: true),
                    ClaseExtraId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensajesEnviados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MensajesEnviados_ClasesExtras_ClaseExtraId",
                        column: x => x.ClaseExtraId,
                        principalTable: "ClasesExtras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MensajesEnviados_Tutores_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClasesExtras_EstudianteId",
                table: "ClasesExtras",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClasesExtras_ModuloId",
                table: "ClasesExtras",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_ClasesExtras_TutorId",
                table: "ClasesExtras",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_TutorId",
                table: "Estudiantes",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_MensajesEnviados_ClaseExtraId",
                table: "MensajesEnviados",
                column: "ClaseExtraId");

            migrationBuilder.CreateIndex(
                name: "IX_MensajesEnviados_TutorId",
                table: "MensajesEnviados",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_TutorId",
                table: "Notificaciones",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenciasTutores_TutorId",
                table: "PreferenciasTutores",
                column: "TutorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MensajesEnviados");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "PreferenciasTutores");

            migrationBuilder.DropTable(
                name: "ClasesExtras");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "Tutores");
        }
    }
}
