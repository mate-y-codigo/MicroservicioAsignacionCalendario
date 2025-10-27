using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroservicioAsignacionCalendario.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initMicro : Migration
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
                    IdPlanEntrenamiento = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSesionActual = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IntervaloDiasDescanso = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
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
                    IdAlumno = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEntrenador = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSesionEntrenamiento = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Notas = table.Column<string>(type: "text", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    FechaActualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoCalendario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecordPersonal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdAlumno = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEjercicio = table.Column<Guid>(type: "uuid", nullable: false),
                    PesoMax = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Series = table.Column<int>(type: "int", nullable: false),
                    Repeticiones = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordPersonal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SesionRealizada",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSesionEntrenamiento = table.Column<Guid>(type: "uuid", nullable: false),
                    IdPlanAlumno = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaRealizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Estado = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionRealizada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SesionRealizada_AlumnoPlan_IdPlanAlumno",
                        column: x => x.IdPlanAlumno,
                        principalTable: "AlumnoPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EjercicioRegistro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSesionRealizada = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEjercicio = table.Column<Guid>(type: "uuid", nullable: false),
                    Series = table.Column<int>(type: "int", nullable: false),
                    Repeticiones = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Completado = table.Column<bool>(type: "bool", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EjercicioRegistro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EjercicioRegistro_SesionRealizada_IdSesionRealizada",
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
                name: "IX_SesionRealizada_IdPlanAlumno",
                table: "SesionRealizada",
                column: "IdPlanAlumno");
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
