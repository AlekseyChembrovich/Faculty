using Microsoft.EntityFrameworkCore.Migrations;

namespace Faculty.DataAccessLayer.Migrations
{
    public partial class m3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDateAducation",
                table: "Faculties",
                newName: "StartDateEducation");

            migrationBuilder.RenameColumn(
                name: "CountYearAducation",
                table: "Faculties",
                newName: "CountYearEducation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDateEducation",
                table: "Faculties",
                newName: "StartDateAducation");

            migrationBuilder.RenameColumn(
                name: "CountYearEducation",
                table: "Faculties",
                newName: "CountYearAducation");
        }
    }
}
