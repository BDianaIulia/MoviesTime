using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTime.DataAccessLibrary.Migrations
{
    public partial class photoPathUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "User");
        }
    }
}
