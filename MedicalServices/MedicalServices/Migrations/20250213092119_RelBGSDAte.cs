using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalServices.Migrations
{
    /// <inheritdoc />
    public partial class RelBGSDAte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DateID",
                table: "BranchGusetServices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BranchGusetServices_DateID",
                table: "BranchGusetServices",
                column: "DateID");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchGusetServices_Dates_DateID",
                table: "BranchGusetServices",
                column: "DateID",
                principalTable: "Dates",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchGusetServices_Dates_DateID",
                table: "BranchGusetServices");

            migrationBuilder.DropIndex(
                name: "IX_BranchGusetServices_DateID",
                table: "BranchGusetServices");

            migrationBuilder.DropColumn(
                name: "DateID",
                table: "BranchGusetServices");
        }
    }
}
