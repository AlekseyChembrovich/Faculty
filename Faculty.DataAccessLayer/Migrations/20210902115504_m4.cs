using Microsoft.EntityFrameworkCore.Migrations;

namespace Faculty.DataAccessLayer.Migrations
{
    public partial class m4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Specialozations_SpecializationId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialozations",
                table: "Specialozations");

            migrationBuilder.RenameTable(
                name: "Specialozations",
                newName: "Specializations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specializations",
                table: "Specializations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Specializations_SpecializationId",
                table: "Groups",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Specializations_SpecializationId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specializations",
                table: "Specializations");

            migrationBuilder.RenameTable(
                name: "Specializations",
                newName: "Specialozations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialozations",
                table: "Specialozations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Specialozations_SpecializationId",
                table: "Groups",
                column: "SpecializationId",
                principalTable: "Specialozations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
