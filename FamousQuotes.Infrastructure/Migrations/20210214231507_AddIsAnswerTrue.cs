using Microsoft.EntityFrameworkCore.Migrations;

namespace FamousQuotes.Infrastructure.Migrations
{
    public partial class AddIsAnswerTrue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAnswerTrue",
                table: "Answer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAnswerTrue",
                table: "Answer");
        }
    }
}
