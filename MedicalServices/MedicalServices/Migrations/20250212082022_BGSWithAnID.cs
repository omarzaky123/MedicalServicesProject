using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalServices.Migrations
{
    /// <inheritdoc />
    public partial class BGSWithAnID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchGusetServices_Branchs_BranchID",
                table: "BranchGusetServices");

            migrationBuilder.DropForeignKey(
                name: "FK_BranchGusetServices_Gusets_GusetID",
                table: "BranchGusetServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BranchGusetServices",
                table: "BranchGusetServices");

            migrationBuilder.AlterColumn<int>(
                name: "GusetID",
                table: "BranchGusetServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BranchID",
                table: "BranchGusetServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "BranchGusetServices",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchGusetServices",
                table: "BranchGusetServices",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_BranchGusetServices_BranchID",
                table: "BranchGusetServices",
                column: "BranchID");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchGusetServices_Branchs_BranchID",
                table: "BranchGusetServices",
                column: "BranchID",
                principalTable: "Branchs",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchGusetServices_Gusets_GusetID",
                table: "BranchGusetServices",
                column: "GusetID",
                principalTable: "Gusets",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchGusetServices_Branchs_BranchID",
                table: "BranchGusetServices");

            migrationBuilder.DropForeignKey(
                name: "FK_BranchGusetServices_Gusets_GusetID",
                table: "BranchGusetServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BranchGusetServices",
                table: "BranchGusetServices");

            migrationBuilder.DropIndex(
                name: "IX_BranchGusetServices_BranchID",
                table: "BranchGusetServices");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "BranchGusetServices");

            migrationBuilder.AlterColumn<int>(
                name: "GusetID",
                table: "BranchGusetServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BranchID",
                table: "BranchGusetServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchGusetServices",
                table: "BranchGusetServices",
                columns: new[] { "BranchID", "GusetID" });

            migrationBuilder.AddForeignKey(
                name: "FK_BranchGusetServices_Branchs_BranchID",
                table: "BranchGusetServices",
                column: "BranchID",
                principalTable: "Branchs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BranchGusetServices_Gusets_GusetID",
                table: "BranchGusetServices",
                column: "GusetID",
                principalTable: "Gusets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
