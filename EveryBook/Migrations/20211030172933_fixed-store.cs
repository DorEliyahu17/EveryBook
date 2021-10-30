using Microsoft.EntityFrameworkCore.Migrations;

namespace EveryBook.Migrations
{
    public partial class fixedstore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Store");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Store",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
