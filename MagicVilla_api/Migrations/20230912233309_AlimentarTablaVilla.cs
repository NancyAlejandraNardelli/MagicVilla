﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_api.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle de la Villa....", new DateTime(2023, 9, 12, 20, 33, 9, 538, DateTimeKind.Local).AddTicks(4295), new DateTime(2023, 9, 12, 20, 33, 9, 538, DateTimeKind.Local).AddTicks(4282), "", 50.0, "Villa Real", 5, 200.0 },
                    { 2, "", "Premium vista a la Piscina", new DateTime(2023, 9, 12, 20, 33, 9, 538, DateTimeKind.Local).AddTicks(4298), new DateTime(2023, 9, 12, 20, 33, 9, 538, DateTimeKind.Local).AddTicks(4297), "", 40.0, "Villa Real", 4, 150.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
