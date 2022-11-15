using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class forth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Status_Students_CustomerExchangeID",
                table: "Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "CustomerExchange");

            migrationBuilder.AddColumn<float>(
                name: "ConvertedValue",
                table: "CustomerExchange",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerExchange",
                table: "CustomerExchange",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Status_CustomerExchange_CustomerExchangeID",
                table: "Status",
                column: "CustomerExchangeID",
                principalTable: "CustomerExchange",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Status_CustomerExchange_CustomerExchangeID",
                table: "Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerExchange",
                table: "CustomerExchange");

            migrationBuilder.DropColumn(
                name: "ConvertedValue",
                table: "CustomerExchange");

            migrationBuilder.RenameTable(
                name: "CustomerExchange",
                newName: "Students");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Status_Students_CustomerExchangeID",
                table: "Status",
                column: "CustomerExchangeID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
