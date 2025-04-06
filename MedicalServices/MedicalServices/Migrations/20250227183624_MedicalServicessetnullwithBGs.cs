using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalServices.Migrations
{
    /// <inheritdoc />
    public partial class MedicalServicessetnullwithBGs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchGusetServices_MedicalServices_ServiceID",
                table: "BranchGusetServices");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchGusetServices_MedicalServices_ServiceID",
                table: "BranchGusetServices",
                column: "ServiceID",
                principalTable: "MedicalServices",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchGusetServices_MedicalServices_ServiceID",
                table: "BranchGusetServices");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchGusetServices_MedicalServices_ServiceID",
                table: "BranchGusetServices",
                column: "ServiceID",
                principalTable: "MedicalServices",
                principalColumn: "ID");
        }
    }
}
