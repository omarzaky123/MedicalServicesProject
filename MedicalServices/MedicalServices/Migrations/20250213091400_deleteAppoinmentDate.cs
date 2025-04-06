using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalServices.Migrations
{
    /// <inheritdoc />
    public partial class deleteAppoinmentDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppoinmentDate",
                table: "BranchGusetServices");

            migrationBuilder.DropColumn(
                name: "State",
                table: "BranchGusetServices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AppoinmentDate",
                table: "BranchGusetServices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "State",
                table: "BranchGusetServices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
