using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pitcher.Migrations
{
    public partial class tblAccessTokenCache : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tblResult",
                table: "tblResult");

            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "tblResult",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblResult",
                table: "tblResult",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "tblAccessTokenCache",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(449)", maxLength: 449, nullable: false),
                    Value = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ExpiresAtTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SlidingExpirationInSeconds = table.Column<long>(type: "bigint", nullable: true),
                    AbsoluteExpiration = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAccessTokenCache", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblResult_JobID",
                table: "tblResult",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_tblAccessTokenCache_ExpiresAtTime",
                schema: "security",
                table: "tblAccessTokenCache",
                column: "ExpiresAtTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAccessTokenCache",
                schema: "security");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblResult",
                table: "tblResult");

            migrationBuilder.DropIndex(
                name: "IX_tblResult_JobID",
                table: "tblResult");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "tblResult");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblResult",
                table: "tblResult",
                columns: new[] { "JobID", "ProblemID" });
        }
    }
}
