using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MatchList.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "matchauditlogs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    eventid = table.Column<long>(type: "bigint", nullable: false),
                    affectedcolumns = table.Column<string>(type: "text", nullable: true),
                    oldvalues = table.Column<string>(type: "text", nullable: true),
                    newvalues = table.Column<string>(type: "text", nullable: true),
                    auditon = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_matchauditlogs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    eventid = table.Column<long>(type: "bigint", nullable: false),
                    eventtype = table.Column<int>(type: "integer", nullable: false),
                    country = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    league = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    hometeam = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    awayteam = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    eventtime = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_matches", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_matchauditlogs_eventid",
                table: "matchauditlogs",
                column: "eventid");

            migrationBuilder.CreateIndex(
                name: "ix_matches_eventid",
                table: "matches",
                column: "eventid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matchauditlogs");

            migrationBuilder.DropTable(
                name: "matches");
        }
    }
}
