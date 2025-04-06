using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalServices.Migrations
{
    /// <inheritdoc />
    public partial class FKAllowNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accountants_Branchs_BranchID",
                table: "Accountants");

            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Branchs_BranchID",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Assastants_Branchs_BranchID",
                table: "Assastants");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Branchs_BranchID",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalServices_Branchs_BranchID",
                table: "MedicalServices");

            migrationBuilder.AlterColumn<int>(
                name: "BranchID",
                table: "MedicalServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BranchID",
                table: "Doctors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BranchID",
                table: "Assastants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BranchID",
                table: "Admins",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BranchID",
                table: "Accountants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Accountants_Branchs_BranchID",
                table: "Accountants",
                column: "BranchID",
                principalTable: "Branchs",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Branchs_BranchID",
                table: "Admins",
                column: "BranchID",
                principalTable: "Branchs",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Assastants_Branchs_BranchID",
                table: "Assastants",
                column: "BranchID",
                principalTable: "Branchs",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Branchs_BranchID",
                table: "Doctors",
                column: "BranchID",
                principalTable: "Branchs",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalServices_Branchs_BranchID",
                table: "MedicalServices",
                column: "BranchID",
                principalTable: "Branchs",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accountants_Branchs_BranchID",
                table: "Accountants");

            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Branchs_BranchID",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Assastants_Branchs_BranchID",
                table: "Assastants");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Branchs_BranchID",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalServices_Branchs_BranchID",
                table: "MedicalServices");

            migrationBuilder.AlterColumn<int>(
                name: "BranchID",
                table: "MedicalServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BranchID",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BranchID",
                table: "Assastants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BranchID",
                table: "Admins",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BranchID",
                table: "Accountants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accountants_Branchs_BranchID",
                table: "Accountants",
                column: "BranchID",
                principalTable: "Branchs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Branchs_BranchID",
                table: "Admins",
                column: "BranchID",
                principalTable: "Branchs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assastants_Branchs_BranchID",
                table: "Assastants",
                column: "BranchID",
                principalTable: "Branchs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Branchs_BranchID",
                table: "Doctors",
                column: "BranchID",
                principalTable: "Branchs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalServices_Branchs_BranchID",
                table: "MedicalServices",
                column: "BranchID",
                principalTable: "Branchs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
