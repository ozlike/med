using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTask.Migrations
{
    public partial class AddLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Grafts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Grafts_PatientId",
                table: "Grafts",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grafts_Patients_PatientId",
                table: "Grafts",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grafts_Patients_PatientId",
                table: "Grafts");

            migrationBuilder.DropIndex(
                name: "IX_Grafts_PatientId",
                table: "Grafts");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Grafts");
        }
    }
}
