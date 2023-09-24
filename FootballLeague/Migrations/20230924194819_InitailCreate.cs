using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballLeagueLib.Migrations
{
    /// <inheritdoc />
    public partial class InitailCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    IdClub = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StadiumName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GoalsScored = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    GoalsConceded = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    GoalBalance = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Wins = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Draws = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Failures = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Points = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.IdClub);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    IdMatch = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeTeamName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AwayTeamName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    MatchName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    MatchDate = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    GoalsHomeTeam = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    GoalsAwayTeam = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    Result = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: " - "),
                    IsPlayed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Round = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    HomeTeamId = table.Column<int>(type: "int", nullable: false),
                    AwayTeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.IdMatch);
                    table.ForeignKey(
                        name: "PlayAwayTeam",
                        column: x => x.AwayTeamId,
                        principalTable: "Clubs",
                        principalColumn: "IdClub");
                    table.ForeignKey(
                        name: "PlayHomeTeam",
                        column: x => x.HomeTeamId,
                        principalTable: "Clubs",
                        principalColumn: "IdClub");
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    IdPlayer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PESEL = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    ShirtNumber = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    GoalsScored = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((0))"),
                    ClubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.IdPlayer);
                    table.ForeignKey(
                        name: "PlayFor",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "IdClub");
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    IdGoal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinuteOfTheMatch = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.IdGoal);
                    table.ForeignKey(
                        name: "Shoot",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "IdMatch");
                    table.ForeignKey(
                        name: "Shoot by",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "IdPlayer");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goals_MatchId",
                table: "Goals",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_PlayerId",
                table: "Goals",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AwayTeamId",
                table: "Matches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_ClubId",
                table: "Players",
                column: "ClubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Clubs");
        }
    }
}
