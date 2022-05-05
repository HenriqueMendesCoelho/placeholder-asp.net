using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceHolder.Migrations
{
    public partial class districtToUserAddressTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "users_address",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "users_address");
        }
    }
}
