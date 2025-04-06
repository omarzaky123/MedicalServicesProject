using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalServices.Migrations
{
    /// <inheritdoc />
    public partial class datebranchBGSRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DateBranchID",
                table: "BranchGusetServices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BranchGusetServices_DateBranchID",
                table: "BranchGusetServices",
                column: "DateBranchID");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchGusetServices_DateBranchs_DateBranchID",
                table: "BranchGusetServices",
                column: "DateBranchID",
                principalTable: "DateBranchs",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchGusetServices_DateBranchs_DateBranchID",
                table: "BranchGusetServices");

            migrationBuilder.DropIndex(
                name: "IX_BranchGusetServices_DateBranchID",
                table: "BranchGusetServices");

            migrationBuilder.DropColumn(
                name: "DateBranchID",
                table: "BranchGusetServices");
        }
    }
}
