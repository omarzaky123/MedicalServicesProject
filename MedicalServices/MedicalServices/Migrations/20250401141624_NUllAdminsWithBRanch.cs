using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalServices.Migrations
{
    /// <inheritdoc />
    public partial class NUllAdminsWithBRanch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Branchs_BranchID",
                table: "Admins");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Branchs_BranchID",
                table: "Admins",
                column: "BranchID",
                principalTable: "Branchs",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Branchs_BranchID",
                table: "Admins");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Branchs_BranchID",
                table: "Admins",
                column: "BranchID",
                principalTable: "Branchs",
                principalColumn: "ID");
        }
    }
}
