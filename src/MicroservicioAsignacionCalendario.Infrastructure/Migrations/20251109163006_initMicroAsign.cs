using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroservicioAsignacionCalendario.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initMicroAsign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombreEjercicio",
                table: "RecordPersonal",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "IdAlumnoPlan",
                table: "EventoCalendario",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "NombreEjercicio",
                table: "EjercicioRegistro",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreEjercicio",
                table: "RecordPersonal");

            migrationBuilder.DropColumn(
                name: "IdAlumnoPlan",
                table: "EventoCalendario");

            migrationBuilder.DropColumn(
                name: "NombreEjercicio",
                table: "EjercicioRegistro");
        }
    }
}
