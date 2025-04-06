using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalServices.Migrations
{
    /// <inheritdoc />
    public partial class RemoveState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchGusetServices_Dates_DateID",
                table: "BranchGusetServices");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Dates");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchGusetServices_Dates_DateID",
                table: "BranchGusetServices",
                column: "DateID",
                principalTable: "Dates",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchGusetServices_Dates_DateID",
                table: "BranchGusetServices");

            migrationBuilder.AddColumn<bool>(
                name: "State",
                table: "Dates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_BranchGusetServices_Dates_DateID",
                table: "BranchGusetServices",
                column: "DateID",
                principalTable: "Dates",
                principalColumn: "ID");
        }
    }
}
