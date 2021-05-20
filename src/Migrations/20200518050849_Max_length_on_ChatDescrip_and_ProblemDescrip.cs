using Microsoft.EntityFrameworkCore.Migrations;

namespace Pitcher.Migrations
{
    public partial class Max_length_on_ChatDescrip_and_ProblemDescrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProblemDescription",
                table: "tblProblem",
                maxLength: 2147483647,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ChatDescription",
                table: "tblChat",
                maxLength: 2147483647,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProblemDescription",
                table: "tblProblem",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2147483647);

            migrationBuilder.AlterColumn<string>(
                name: "ChatDescription",
                table: "tblChat",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2147483647);
        }
    }
}
