using Microsoft.EntityFrameworkCore.Migrations;

namespace FamousQuotes.Infrastructure.Migrations
{
    public partial class AddAuthorNameAndQuoteText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Quotes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Authors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Authors");
        }
    }
}
