using Microsoft.EntityFrameworkCore.Migrations;

namespace FamousQuotes.Infrastructure.Migrations
{
    public partial class RemoveUserIdFromAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Users_UserId1",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_UserId1",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Answer");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Answer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_UserId",
                table: "Answer",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Users_UserId",
                table: "Answer",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Users_UserId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_UserId",
                table: "Answer");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Answer",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Answer",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answer_UserId1",
                table: "Answer",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Users_UserId1",
                table: "Answer",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
