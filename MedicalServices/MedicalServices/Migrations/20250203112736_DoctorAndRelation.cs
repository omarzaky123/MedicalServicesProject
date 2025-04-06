using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalServices.Migrations
{
    /// <inheritdoc />
    public partial class DoctorAndRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Doctors_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Branchs_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branchs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_BranchID",
                table: "Doctors",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserID",
                table: "Doctors",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctors");
        }
    }
}
