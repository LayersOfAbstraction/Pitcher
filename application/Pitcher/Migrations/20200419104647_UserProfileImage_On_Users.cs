using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pitcher.Migrations
{
    public partial class UserProfileImage_On_Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserPhoneNumber",
                table: "tblUser",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "UserMobileNumber",
                table: "tblUser",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<string>(
                name: "UserProfileImage",
                table: "tblUser",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Problem",
                columns: table => new
                {
                    ProblemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JobID = table.Column<int>(nullable: false),
                    RegistrationID = table.Column<int>(nullable: false),
                    ProblemTitle = table.Column<string>(maxLength: 180, nullable: false),
                    ProblemDescription = table.Column<string>(maxLength: 255, nullable: false),
                    ProblemStartDate = table.Column<DateTime>(nullable: false),
                    ProblemFileAttachments = table.Column<string>(nullable: true),
                    ProblemSeverity = table.Column<int>(nullable: false),
                    ProblemComplete = table.Column<bool>(nullable: false),
                    UserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problem", x => x.ProblemID);
                    table.ForeignKey(
                        name: "FK_Problem_tblJob_JobID",
                        column: x => x.JobID,
                        principalTable: "tblJob",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Problem_tblUser_UserID",
                        column: x => x.UserID,
                        principalTable: "tblUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Problem_JobID",
                table: "Problem",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_Problem_UserID",
                table: "Problem",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Problem");

            migrationBuilder.DropColumn(
                name: "UserProfileImage",
                table: "tblUser");

            migrationBuilder.AlterColumn<long>(
                name: "UserPhoneNumber",
                table: "tblUser",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UserMobileNumber",
                table: "tblUser",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
