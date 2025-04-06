using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalServices.Migrations
{
    /// <inheritdoc />
    public partial class CatigoryMedicalRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatigoryID",
                table: "MedicalServices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Catigorys",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catigorys", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServices_CatigoryID",
                table: "MedicalServices",
                column: "CatigoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalServices_Catigorys_CatigoryID",
                table: "MedicalServices",
                column: "CatigoryID",
                principalTable: "Catigorys",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalServices_Catigorys_CatigoryID",
                table: "MedicalServices");

            migrationBuilder.DropTable(
                name: "Catigorys");

            migrationBuilder.DropIndex(
                name: "IX_MedicalServices_CatigoryID",
                table: "MedicalServices");

            migrationBuilder.DropColumn(
                name: "CatigoryID",
                table: "MedicalServices");
        }
    }
}
