using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsWebsite.Migrations
{
    public partial class CreatedEnumsAcceptedRejected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Informations",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Informations");
        }
    }
}
