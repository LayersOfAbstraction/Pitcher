using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pitcher.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblJob",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JobTitle = table.Column<string>(maxLength: 20, nullable: false),
                    JobDescription = table.Column<string>(maxLength: 200, nullable: true),
                    JobStartDate = table.Column<DateTime>(nullable: false),
                    JobDeadlineDate = table.Column<DateTime>(nullable: false),
                    JobIsComplete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblJob", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblUser",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserFirstName = table.Column<string>(maxLength: 20, nullable: false),
                    UserLastName = table.Column<string>(maxLength: 30, nullable: false),
                    UserIsLeader = table.Column<bool>(nullable: false),
                    UserContactEmail = table.Column<string>(maxLength: 30, nullable: false),
                    UserPhoneNumber = table.Column<long>(nullable: false),
                    UserAddress = table.Column<string>(maxLength: 37, nullable: true),
                    UserPostCode = table.Column<string>(nullable: true),
                    UserCountry = table.Column<string>(maxLength: 15, nullable: true),
                    UserMobileNumber = table.Column<long>(nullable: false),
                    UserState = table.Column<string>(maxLength: 3, nullable: true),
                    UserLogInName = table.Column<string>(maxLength: 16, nullable: false),
                    UserPassword = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblRegistration",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    JobID = table.Column<int>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRegistration", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblRegistration_tblJob_JobID",
                        column: x => x.JobID,
                        principalTable: "tblJob",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblRegistration_tblUser_UserID",
                        column: x => x.UserID,
                        principalTable: "tblUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblRegistration_JobID",
                table: "tblRegistration",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRegistration_UserID",
                table: "tblRegistration",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblRegistration");

            migrationBuilder.DropTable(
                name: "tblJob");

            migrationBuilder.DropTable(
                name: "tblUser");
        }
    }
}
