using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalServices.Migrations
{
    public partial class BranchGusetServicesAndRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BranchGusetServices",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchID = table.Column<int>(type: "int", nullable: true),
                    GusetID = table.Column<int>(type: "int", nullable: true),
                    ServiceID = table.Column<int>(type: "int", nullable: true),
                    DateBranchID = table.Column<int>(type: "int", nullable: true),
                    EmailSent = table.Column<bool>(type: "bit", nullable: true),
                    Uploaded = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchGusetServices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BranchGusetServices_Branches_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_BranchGusetServices_Gusets_GusetID",
                        column: x => x.GusetID,
                        principalTable: "Gusets",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_BranchGusetServices_MedicalServices_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "MedicalServices",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_BranchGusetServices_DateBranches_DateBranchID",
                        column: x => x.DateBranchID,
                        principalTable: "DateBranches",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BranchGusetServices_BranchID",
                table: "BranchGusetServices",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_BranchGusetServices_GusetID",
                table: "BranchGusetServices",
                column: "GusetID");

            migrationBuilder.CreateIndex(
                name: "IX_BranchGusetServices_ServiceID",
                table: "BranchGusetServices",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_BranchGusetServices_DateBranchID",
                table: "BranchGusetServices",
                column: "DateBranchID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchGusetServices");
        }
    }
}
