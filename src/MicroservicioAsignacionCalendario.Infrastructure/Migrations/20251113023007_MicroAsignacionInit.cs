using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroservicioAsignacionCalendario.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MicroAsignacionInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EjercicioRegistro_SesionRealizada_IdSesionRealizada",
                table: "EjercicioRegistro");

            migrationBuilder.DropForeignKey(
                name: "FK_SesionRealizada_AlumnoPlan_IdPlanAlumno",
                table: "SesionRealizada");

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                table: "EventoCalendario");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "EventoCalendario");

            migrationBuilder.DropColumn(
                name: "FechaFin",
                table: "EventoCalendario");

            migrationBuilder.DropColumn(
                name: "IdAlumno",
                table: "EventoCalendario");

            migrationBuilder.DropColumn(
                name: "IdEntrenador",
                table: "EventoCalendario");

            migrationBuilder.RenameColumn(
                name: "IdPlanAlumno",
                table: "SesionRealizada",
                newName: "IdAlumnoPlan");

            migrationBuilder.RenameIndex(
                name: "IX_SesionRealizada_IdPlanAlumno",
                table: "SesionRealizada",
                newName: "IX_SesionRealizada_IdAlumnoPlan");

            migrationBuilder.RenameColumn(
                name: "FechaInicio",
                table: "EventoCalendario",
                newName: "FechaProgramada");

            migrationBuilder.RenameColumn(
                name: "IdSesionActual",
                table: "AlumnoPlan",
                newName: "IdSesionARealizar");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaRealizacion",
                table: "SesionRealizada",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "SesionRealizada",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AddColumn<decimal>(
                name: "AlturaEnCmAlumno",
                table: "SesionRealizada",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreSesion",
                table: "SesionRealizada",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OrdenSesion",
                table: "SesionRealizada",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PesoCorporalAlumno",
                table: "SesionRealizada",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PesoMax",
                table: "RecordPersonal",
                type: "numeric(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)");

            migrationBuilder.AlterColumn<string>(
                name: "NombreEjercicio",
                table: "RecordPersonal",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaRegistro",
                table: "RecordPersonal",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<decimal>(
                name: "Calculo1RM",
                table: "RecordPersonal",
                type: "numeric(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "NombreGrupoMuscular",
                table: "RecordPersonal",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "EventoCalendario",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "PesoObjetivo",
                table: "EjercicioRegistro",
                type: "numeric(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)");

            migrationBuilder.AlterColumn<string>(
                name: "NombreEjercicio",
                table: "EjercicioRegistro",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "DescansoObjetivo",
                table: "EjercicioRegistro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "IdEjercicioSesion",
                table: "EjercicioRegistro",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "NombreCategoria",
                table: "EjercicioRegistro",
                type: "character varying(25)",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreGrupoMuscular",
                table: "EjercicioRegistro",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreMusculo",
                table: "EjercicioRegistro",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrdenEjercicio",
                table: "EjercicioRegistro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UrlDemoEjercicio",
                table: "EjercicioRegistro",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "AlumnoPlan",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFin",
                table: "AlumnoPlan",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW() + INTERVAL '2 weeks'");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "AlumnoPlan",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "DescripcionPlan",
                table: "AlumnoPlan",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombrePlan",
                table: "AlumnoPlan",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RecordPersonal_IdAlumno_IdEjercicio",
                table: "RecordPersonal",
                columns: new[] { "IdAlumno", "IdEjercicio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventoCalendario_IdAlumnoPlan",
                table: "EventoCalendario",
                column: "IdAlumnoPlan");

            migrationBuilder.AddForeignKey(
                name: "FK_EjercicioRegistro_SesionRealizada_IdSesionRealizada",
                table: "EjercicioRegistro",
                column: "IdSesionRealizada",
                principalTable: "SesionRealizada",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventoCalendario_AlumnoPlan_IdAlumnoPlan",
                table: "EventoCalendario",
                column: "IdAlumnoPlan",
                principalTable: "AlumnoPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SesionRealizada_AlumnoPlan_IdAlumnoPlan",
                table: "SesionRealizada",
                column: "IdAlumnoPlan",
                principalTable: "AlumnoPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EjercicioRegistro_SesionRealizada_IdSesionRealizada",
                table: "EjercicioRegistro");

            migrationBuilder.DropForeignKey(
                name: "FK_EventoCalendario_AlumnoPlan_IdAlumnoPlan",
                table: "EventoCalendario");

            migrationBuilder.DropForeignKey(
                name: "FK_SesionRealizada_AlumnoPlan_IdAlumnoPlan",
                table: "SesionRealizada");

            migrationBuilder.DropIndex(
                name: "IX_RecordPersonal_IdAlumno_IdEjercicio",
                table: "RecordPersonal");

            migrationBuilder.DropIndex(
                name: "IX_EventoCalendario_IdAlumnoPlan",
                table: "EventoCalendario");

            migrationBuilder.DropColumn(
                name: "AlturaEnCmAlumno",
                table: "SesionRealizada");

            migrationBuilder.DropColumn(
                name: "NombreSesion",
                table: "SesionRealizada");

            migrationBuilder.DropColumn(
                name: "OrdenSesion",
                table: "SesionRealizada");

            migrationBuilder.DropColumn(
                name: "PesoCorporalAlumno",
                table: "SesionRealizada");

            migrationBuilder.DropColumn(
                name: "Calculo1RM",
                table: "RecordPersonal");

            migrationBuilder.DropColumn(
                name: "NombreGrupoMuscular",
                table: "RecordPersonal");

            migrationBuilder.DropColumn(
                name: "DescansoObjetivo",
                table: "EjercicioRegistro");

            migrationBuilder.DropColumn(
                name: "IdEjercicioSesion",
                table: "EjercicioRegistro");

            migrationBuilder.DropColumn(
                name: "NombreCategoria",
                table: "EjercicioRegistro");

            migrationBuilder.DropColumn(
                name: "NombreGrupoMuscular",
                table: "EjercicioRegistro");

            migrationBuilder.DropColumn(
                name: "NombreMusculo",
                table: "EjercicioRegistro");

            migrationBuilder.DropColumn(
                name: "OrdenEjercicio",
                table: "EjercicioRegistro");

            migrationBuilder.DropColumn(
                name: "UrlDemoEjercicio",
                table: "EjercicioRegistro");

            migrationBuilder.DropColumn(
                name: "DescripcionPlan",
                table: "AlumnoPlan");

            migrationBuilder.DropColumn(
                name: "NombrePlan",
                table: "AlumnoPlan");

            migrationBuilder.RenameColumn(
                name: "IdAlumnoPlan",
                table: "SesionRealizada",
                newName: "IdPlanAlumno");

            migrationBuilder.RenameIndex(
                name: "IX_SesionRealizada_IdAlumnoPlan",
                table: "SesionRealizada",
                newName: "IX_SesionRealizada_IdPlanAlumno");

            migrationBuilder.RenameColumn(
                name: "FechaProgramada",
                table: "EventoCalendario",
                newName: "FechaInicio");

            migrationBuilder.RenameColumn(
                name: "IdSesionARealizar",
                table: "AlumnoPlan",
                newName: "IdSesionActual");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaRealizacion",
                table: "SesionRealizada",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "Estado",
                table: "SesionRealizada",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "PesoMax",
                table: "RecordPersonal",
                type: "numeric(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(6,2)",
                oldPrecision: 6,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "NombreEjercicio",
                table: "RecordPersonal",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaRegistro",
                table: "RecordPersonal",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "Estado",
                table: "EventoCalendario",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                table: "EventoCalendario",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "EventoCalendario",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFin",
                table: "EventoCalendario",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "IdAlumno",
                table: "EventoCalendario",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdEntrenador",
                table: "EventoCalendario",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "PesoObjetivo",
                table: "EjercicioRegistro",
                type: "numeric(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(6,2)",
                oldPrecision: 6,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "NombreEjercicio",
                table: "EjercicioRegistro",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "AlumnoPlan",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFin",
                table: "AlumnoPlan",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW() + INTERVAL '2 weeks'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "Estado",
                table: "AlumnoPlan",
                type: "integer",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_EjercicioRegistro_SesionRealizada_IdSesionRealizada",
                table: "EjercicioRegistro",
                column: "IdSesionRealizada",
                principalTable: "SesionRealizada",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SesionRealizada_AlumnoPlan_IdPlanAlumno",
                table: "SesionRealizada",
                column: "IdPlanAlumno",
                principalTable: "AlumnoPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
