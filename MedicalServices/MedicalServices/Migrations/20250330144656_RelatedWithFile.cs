using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalServices.Migrations
{
    /// <inheritdoc />
    public partial class RelatedWithFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BGSID",
                table: "UplodedFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UplodedFiles_BGSID",
                table: "UplodedFiles",
                column: "BGSID");

            migrationBuilder.AddForeignKey(
                name: "FK_UplodedFiles_BranchGusetServices_BGSID",
                table: "UplodedFiles",
                column: "BGSID",
                principalTable: "BranchGusetServices",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UplodedFiles_BranchGusetServices_BGSID",
                table: "UplodedFiles");

            migrationBuilder.DropIndex(
                name: "IX_UplodedFiles_BGSID",
                table: "UplodedFiles");

            migrationBuilder.DropColumn(
                name: "BGSID",
                table: "UplodedFiles");
        }
    }
}
