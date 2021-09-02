using Microsoft.EntityFrameworkCore.Migrations;

namespace Faculty.DataAccessLayer.Migrations
{
    public partial class m2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Specialozations_SpecialozationId",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "SpecialozationId",
                table: "Groups",
                newName: "SpecializationId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_SpecialozationId",
                table: "Groups",
                newName: "IX_Groups_SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Specialozations_SpecializationId",
                table: "Groups",
                column: "SpecializationId",
                principalTable: "Specialozations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Specialozations_SpecializationId",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "SpecializationId",
                table: "Groups",
                newName: "SpecialozationId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_SpecializationId",
                table: "Groups",
                newName: "IX_Groups_SpecialozationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Specialozations_SpecialozationId",
                table: "Groups",
                column: "SpecialozationId",
                principalTable: "Specialozations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
