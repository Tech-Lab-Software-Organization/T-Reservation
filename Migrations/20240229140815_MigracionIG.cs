using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T_Reservation.Migrations
{
    public partial class MigracionIG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Imagen",
                table: "Restaurantes",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Restaurantes");
        }
    }
}
