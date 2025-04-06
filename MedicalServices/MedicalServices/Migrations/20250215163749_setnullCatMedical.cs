using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalServices.Migrations
{
    /// <inheritdoc />
    public partial class setnullCatMedical : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalServices_Catigorys_CatigoryID",
                table: "MedicalServices");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalServices_Catigorys_CatigoryID",
                table: "MedicalServices",
                column: "CatigoryID",
                principalTable: "Catigorys",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalServices_Catigorys_CatigoryID",
                table: "MedicalServices");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalServices_Catigorys_CatigoryID",
                table: "MedicalServices",
                column: "CatigoryID",
                principalTable: "Catigorys",
                principalColumn: "ID");
        }
    }
}
