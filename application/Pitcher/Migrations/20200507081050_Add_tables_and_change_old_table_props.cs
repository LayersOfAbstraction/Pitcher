using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pitcher.Migrations
{
    public partial class Add_tables_and_change_old_table_props : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problem_tblJob_JobID",
                table: "Problem");

            migrationBuilder.DropForeignKey(
                name: "FK_Problem_tblUser_UserID",
                table: "Problem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Problem",
                table: "Problem");

            migrationBuilder.DropIndex(
                name: "IX_Problem_UserID",
                table: "Problem");

            migrationBuilder.DropColumn(
                name: "UserIsLeader",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "UserLogInName",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "UserPassword",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "UserProfileImage",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "RegistrationID",
                table: "Problem");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Problem");

            migrationBuilder.RenameTable(
                name: "Problem",
                newName: "tblProblem");

            migrationBuilder.RenameIndex(
                name: "IX_Problem_JobID",
                table: "tblProblem",
                newName: "IX_tblProblem_JobID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblProblem",
                table: "tblProblem",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "tblChat",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProblemID = table.Column<int>(nullable: false),
                    ChatDescription = table.Column<string>(maxLength: 255, nullable: false),
                    ProblemStartDate = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblChat", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblChat_tblProblem_ProblemID",
                        column: x => x.ProblemID,
                        principalTable: "tblProblem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblChat_ProblemID",
                table: "tblChat",
                column: "ProblemID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProblem_tblJob_JobID",
                table: "tblProblem",
                column: "JobID",
                principalTable: "tblJob",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProblem_tblJob_JobID",
                table: "tblProblem");

            migrationBuilder.DropTable(
                name: "tblChat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblProblem",
                table: "tblProblem");

            migrationBuilder.RenameTable(
                name: "tblProblem",
                newName: "Problem");

            migrationBuilder.RenameIndex(
                name: "IX_tblProblem_JobID",
                table: "Problem",
                newName: "IX_Problem_JobID");

            migrationBuilder.AddColumn<bool>(
                name: "UserIsLeader",
                table: "tblUser",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserLogInName",
                table: "tblUser",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserPassword",
                table: "tblUser",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserProfileImage",
                table: "tblUser",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegistrationID",
                table: "Problem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Problem",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Problem",
                table: "Problem",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Problem_UserID",
                table: "Problem",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Problem_tblJob_JobID",
                table: "Problem",
                column: "JobID",
                principalTable: "tblJob",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Problem_tblUser_UserID",
                table: "Problem",
                column: "UserID",
                principalTable: "tblUser",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
