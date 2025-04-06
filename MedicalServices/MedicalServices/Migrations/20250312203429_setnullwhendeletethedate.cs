using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalServices.Migrations
{
    /// <inheritdoc />
    public partial class setnullwhendeletethedate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchGusetServices_DateBranchs_DateBranchID",
                table: "BranchGusetServices");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchGusetServices_DateBranchs_DateBranchID",
                table: "BranchGusetServices",
                column: "DateBranchID",
                principalTable: "DateBranchs",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchGusetServices_DateBranchs_DateBranchID",
                table: "BranchGusetServices");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchGusetServices_DateBranchs_DateBranchID",
                table: "BranchGusetServices",
                column: "DateBranchID",
                principalTable: "DateBranchs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
