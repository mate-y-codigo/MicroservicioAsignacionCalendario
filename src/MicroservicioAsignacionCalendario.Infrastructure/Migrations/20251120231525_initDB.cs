using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroservicioAsignacionCalendario.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordPersonal_AlumnoPlan_AlumnoPlanId",
                table: "RecordPersonal");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordPersonal_SesionRealizada_SesionRealizadaId",
                table: "RecordPersonal");

            migrationBuilder.DropIndex(
                name: "IX_RecordPersonal_AlumnoPlanId",
                table: "RecordPersonal");

            migrationBuilder.DropIndex(
                name: "IX_RecordPersonal_SesionRealizadaId",
                table: "RecordPersonal");

            migrationBuilder.DropColumn(
                name: "AlumnoPlanId",
                table: "RecordPersonal");

            migrationBuilder.DropColumn(
                name: "SesionRealizadaId",
                table: "RecordPersonal");

            migrationBuilder.CreateIndex(
                name: "IX_RecordPersonal_IdSesionRealizada",
                table: "RecordPersonal",
                column: "IdSesionRealizada");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordPersonal_SesionRealizada_IdSesionRealizada",
                table: "RecordPersonal",
                column: "IdSesionRealizada",
                principalTable: "SesionRealizada",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordPersonal_SesionRealizada_IdSesionRealizada",
                table: "RecordPersonal");

            migrationBuilder.DropIndex(
                name: "IX_RecordPersonal_IdSesionRealizada",
                table: "RecordPersonal");

            migrationBuilder.AddColumn<Guid>(
                name: "AlumnoPlanId",
                table: "RecordPersonal",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SesionRealizadaId",
                table: "RecordPersonal",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RecordPersonal_AlumnoPlanId",
                table: "RecordPersonal",
                column: "AlumnoPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordPersonal_SesionRealizadaId",
                table: "RecordPersonal",
                column: "SesionRealizadaId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordPersonal_AlumnoPlan_AlumnoPlanId",
                table: "RecordPersonal",
                column: "AlumnoPlanId",
                principalTable: "AlumnoPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordPersonal_SesionRealizada_SesionRealizadaId",
                table: "RecordPersonal",
                column: "SesionRealizadaId",
                principalTable: "SesionRealizada",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
