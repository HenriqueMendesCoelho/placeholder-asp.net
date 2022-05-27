using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceHolder.Migrations
{
    public partial class ServerityFieldOnTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Severity",
                table: "tickets",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Severity",
                table: "tickets");
        }
    }
}
