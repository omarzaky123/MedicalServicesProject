using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalServices.Migrations
{
    /// <inheritdoc />
    public partial class AssAndAccRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accountants",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accountants", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accountants_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accountants_Branchs_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branchs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assastants",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assastants", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Assastants_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assastants_Branchs_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branchs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accountants_BranchID",
                table: "Accountants",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Accountants_UserID",
                table: "Accountants",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Assastants_BranchID",
                table: "Assastants",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Assastants_UserID",
                table: "Assastants",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accountants");

            migrationBuilder.DropTable(
                name: "Assastants");
        }
    }
}
