using Microsoft.EntityFrameworkCore.Migrations;

namespace Pitcher.Migrations
{
    public partial class NewResultRelationshipsInContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProblem_tblJob_JobID",
                table: "tblProblem");

            migrationBuilder.DropIndex(
                name: "IX_tblProblem_JobID",
                table: "tblProblem");

            migrationBuilder.DropColumn(
                name: "JobID",
                table: "tblProblem");

            migrationBuilder.CreateTable(
                name: "tblResult",
                columns: table => new
                {
                    JobID = table.Column<int>(nullable: false),
                    ProblemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblResult", x => new { x.JobID, x.ProblemID });
                    table.ForeignKey(
                        name: "FK_tblResult_tblJob_JobID",
                        column: x => x.JobID,
                        principalTable: "tblJob",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblResult_tblProblem_ProblemID",
                        column: x => x.ProblemID,
                        principalTable: "tblProblem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblResult_ProblemID",
                table: "tblResult",
                column: "ProblemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblResult");

            migrationBuilder.AddColumn<int>(
                name: "JobID",
                table: "tblProblem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblProblem_JobID",
                table: "tblProblem",
                column: "JobID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProblem_tblJob_JobID",
                table: "tblProblem",
                column: "JobID",
                principalTable: "tblJob",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
