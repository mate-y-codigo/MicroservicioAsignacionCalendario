using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroservicioAsignacionCalendario.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlumnoPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdAlumno = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEntrenador = table.Column<Guid>(type: "uuid", nullable: false),
                    IdPlanEntrenamiento = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSesionARealizar = table.Column<Guid>(type: "uuid", nullable: false),
                    NombrePlan = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DescripcionPlan = table.Column<string>(type: "text", nullable: true),
                    TotalSesiones = table.Column<int>(type: "int", nullable: false),
                    TotalEjercicios = table.Column<int>(type: "int", nullable: false),
                    NombreEntrenador = table.Column<string>(type: "text", nullable: false),
                    NombreAlumno = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IntervaloDiasDescanso = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    Notas = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlumnoPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventoCalendario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdAlumnoPlan = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSesionEntrenamiento = table.Column<Guid>(type: "uuid", nullable: false),
                    NombreSesion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    Notas = table.Column<string>(type: "text", nullable: true),
                    FechaProgramada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoCalendario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventoCalendario_AlumnoPlan_IdAlumnoPlan",
                        column: x => x.IdAlumnoPlan,
                        principalTable: "AlumnoPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SesionRealizada",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSesionEntrenamiento = table.Column<Guid>(type: "uuid", nullable: false),
                    IdAlumnoPlan = table.Column<Guid>(type: "uuid", nullable: false),
                    NombreSesion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OrdenSesion = table.Column<int>(type: "int", nullable: false),
                    PesoCorporalAlumno = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    AlturaEnCmAlumno = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    FechaRealizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionRealizada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SesionRealizada_AlumnoPlan_IdAlumnoPlan",
                        column: x => x.IdAlumnoPlan,
                        principalTable: "AlumnoPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EjercicioRegistro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSesionRealizada = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEjercicio = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEjercicioSesion = table.Column<Guid>(type: "uuid", nullable: false),
                    SeriesObjetivo = table.Column<int>(type: "int", nullable: false),
                    RepeticionesObjetivo = table.Column<int>(type: "int", nullable: false),
                    PesoObjetivo = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: false),
                    DescansoObjetivo = table.Column<int>(type: "int", nullable: false),
                    OrdenEjercicio = table.Column<int>(type: "int", nullable: false),
                    NombreEjercicio = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    NombreMusculo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NombreGrupoMuscular = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NombreCategoria = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    UrlDemoEjercicio = table.Column<string>(type: "text", nullable: true),
                    Series = table.Column<int>(type: "int", nullable: false),
                    Repeticiones = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    FechaRealizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EjercicioRegistro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EjercicioRegistro_SesionRealizada_IdSesionRealizada",
                        column: x => x.IdSesionRealizada,
                        principalTable: "SesionRealizada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecordPersonal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdAlumno = table.Column<Guid>(type: "uuid", nullable: false),
                    IdAlumnoPlan = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEjercicio = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSesionRealizada = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEjercicioSesion = table.Column<Guid>(type: "uuid", nullable: false),
                    NombreEjercicio = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NombreGrupoMuscular = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PesoMax = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: false),
                    Series = table.Column<int>(type: "int", nullable: false),
                    Repeticiones = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Calculo1RM = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordPersonal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordPersonal_AlumnoPlan_IdAlumnoPlan",
                        column: x => x.IdAlumnoPlan,
                        principalTable: "AlumnoPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecordPersonal_SesionRealizada_IdSesionRealizada",
                        column: x => x.IdSesionRealizada,
                        principalTable: "SesionRealizada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EjercicioRegistro_IdSesionRealizada",
                table: "EjercicioRegistro",
                column: "IdSesionRealizada");

            migrationBuilder.CreateIndex(
                name: "IX_EventoCalendario_IdAlumnoPlan",
                table: "EventoCalendario",
                column: "IdAlumnoPlan");

            migrationBuilder.CreateIndex(
                name: "IX_RecordPersonal_IdAlumno_IdEjercicio",
                table: "RecordPersonal",
                columns: new[] { "IdAlumno", "IdEjercicio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecordPersonal_IdAlumnoPlan",
                table: "RecordPersonal",
                column: "IdAlumnoPlan");

            migrationBuilder.CreateIndex(
                name: "IX_RecordPersonal_IdSesionRealizada",
                table: "RecordPersonal",
                column: "IdSesionRealizada");

            migrationBuilder.CreateIndex(
                name: "IX_SesionRealizada_IdAlumnoPlan",
                table: "SesionRealizada",
                column: "IdAlumnoPlan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EjercicioRegistro");

            migrationBuilder.DropTable(
                name: "EventoCalendario");

            migrationBuilder.DropTable(
                name: "RecordPersonal");

            migrationBuilder.DropTable(
                name: "SesionRealizada");

            migrationBuilder.DropTable(
                name: "AlumnoPlan");
        }
    }
}
