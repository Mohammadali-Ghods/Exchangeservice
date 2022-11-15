using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.RenameTable(
                name: "Enrollments",
                newName: "Symbol");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Symbol",
                table: "Symbol",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Symbol",
                table: "Symbol");

            migrationBuilder.RenameTable(
                name: "Symbol",
                newName: "Enrollments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                column: "ID");
        }
    }
}
