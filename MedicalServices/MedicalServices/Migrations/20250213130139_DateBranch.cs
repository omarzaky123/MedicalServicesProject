using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalServices.Migrations
{
    /// <inheritdoc />
    public partial class DateBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchGusetServices_Dates_DateID",
                table: "BranchGusetServices");

            migrationBuilder.CreateTable(
                name: "DateBranchs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    DateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateBranchs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DateBranchs_Branchs_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branchs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DateBranchs_Dates_DateId",
                        column: x => x.DateId,
                        principalTable: "Dates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DateBranchs_BranchId",
                table: "DateBranchs",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DateBranchs_DateId",
                table: "DateBranchs",
                column: "DateId");

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

            migrationBuilder.DropTable(
                name: "DateBranchs");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchGusetServices_Dates_DateID",
                table: "BranchGusetServices",
                column: "DateID",
                principalTable: "Dates",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
