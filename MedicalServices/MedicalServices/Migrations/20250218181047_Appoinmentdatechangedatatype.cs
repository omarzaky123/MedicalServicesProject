using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalServices.Migrations
{
    /// <inheritdoc />
    public partial class Appoinmentdatechangedatatype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AppoinmentDate",
                table: "Dates",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AppoinmentDate",
                table: "Dates",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");
        }
    }
}
