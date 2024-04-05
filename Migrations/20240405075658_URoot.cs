using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T_Reservation.Migrations
{
    public partial class URoot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Rol",
                table: "Empleados",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Empleados",
                columns: new[] { "Id", "Correo", "Direccion", "Dui", "FechaNacimiento", "Nombre", "Password", "Rol", "Telefono" },
                values: new object[] { 1, "root@gmail.com", "NINGUNA,NINGUNA", 234555321, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "root", "25d55ad283aa400af464c76d713c07ad", "Administrador", 22229090 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Rol",
                table: "Empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
