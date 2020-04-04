using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTime.DataAccessLibrary.Migrations
{
    public partial class defaultValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewScore",
                table: "Movie");

            migrationBuilder.AlterColumn<int>(
                name: "NumberOf5ReviewStars",
                table: "MovieRating",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NumberOf4ReviewStars",
                table: "MovieRating",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NumberOf3ReviewStars",
                table: "MovieRating",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NumberOf2ReviewStars",
                table: "MovieRating",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NumberOf1ReviewStars",
                table: "MovieRating",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ReviewScoreValue",
                table: "Movie",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewScoreValue",
                table: "Movie");

            migrationBuilder.AlterColumn<int>(
                name: "NumberOf5ReviewStars",
                table: "MovieRating",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "NumberOf4ReviewStars",
                table: "MovieRating",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "NumberOf3ReviewStars",
                table: "MovieRating",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "NumberOf2ReviewStars",
                table: "MovieRating",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "NumberOf1ReviewStars",
                table: "MovieRating",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReviewScore",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
