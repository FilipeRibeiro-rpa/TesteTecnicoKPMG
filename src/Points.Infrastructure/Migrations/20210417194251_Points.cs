using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Points.Infrastructure.Migrations
{
    public partial class Points : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence(
                name: "gameseq",
                schema: "dbo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "games",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    IdentityGuid = table.Column<string>(maxLength: 200, nullable: false),
                    PlayerId = table.Column<long>(nullable: false),
                    GameId = table.Column<long>(nullable: false),
                    Win = table.Column<long>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_games_IdentityGuid",
                schema: "dbo",
                table: "games",
                column: "IdentityGuid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "games",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "requests",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "gameseq",
                schema: "dbo");
        }
    }
}
