using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalServices.Migrations
{
    /// <inheritdoc />
    public partial class ADMINBRanchSetNullRemove : Migration
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
                principalColumn: "ID");
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
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
