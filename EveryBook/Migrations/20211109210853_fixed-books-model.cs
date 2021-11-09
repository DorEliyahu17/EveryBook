using Microsoft.EntityFrameworkCore.Migrations;

namespace EveryBook.Migrations
{
    public partial class fixedbooksmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsGenreDeleted",
                table: "Book",
                newName: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Book",
                newName: "IsGenreDeleted");
        }
    }
}
