using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FNSC.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Viewers",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    display = table.Column<string>(type: "TEXT", nullable: true),
                    subscribed = table.Column<bool>(type: "INTEGER", nullable: false),
                    role = table.Column<int>(type: "INTEGER", nullable: false),
                    type = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viewers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Battles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Song1Id = table.Column<string>(type: "TEXT", nullable: true),
                    Votes1 = table.Column<int>(type: "INTEGER", nullable: false),
                    Song2Id = table.Column<string>(type: "TEXT", nullable: true),
                    Votes2 = table.Column<int>(type: "INTEGER", nullable: false),
                    WinnerId = table.Column<string>(type: "TEXT", nullable: true),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    RoundId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoundId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    RoundId2 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    NoOfRounds = table.Column<int>(type: "INTEGER", nullable: false),
                    PreviewTime = table.Column<int>(type: "INTEGER", nullable: false),
                    VotingTime = table.Column<int>(type: "INTEGER", nullable: false),
                    IsVotingOpen = table.Column<bool>(type: "INTEGER", nullable: false),
                    AllowVoteCorrection = table.Column<bool>(type: "INTEGER", nullable: false),
                    NoOfSongsPerPerson = table.Column<int>(type: "INTEGER", nullable: false),
                    NoOfSongs = table.Column<int>(type: "INTEGER", nullable: false),
                    ChampionshipNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Theme = table.Column<string>(type: "TEXT", nullable: true),
                    SubmissionsOpen = table.Column<bool>(type: "INTEGER", nullable: false),
                    GameFinished = table.Column<bool>(type: "INTEGER", nullable: false),
                    AllowDoubles = table.Column<bool>(type: "INTEGER", nullable: false),
                    MaxSongLength = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    MinSongLength = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    InitTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FinishTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CurrentRoundId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoundNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentBattleNo = table.Column<int>(type: "INTEGER", nullable: false),
                    GameId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rounds_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Channel = table.Column<string>(type: "TEXT", nullable: true),
                    Length = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    IsBlocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true),
                    Viewerid = table.Column<string>(type: "TEXT", nullable: true),
                    InitialStarttime = table.Column<int>(type: "INTEGER", nullable: false),
                    Starttime = table.Column<int>(type: "INTEGER", nullable: false),
                    GameId = table.Column<string>(type: "TEXT", nullable: true),
                    GameId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Songs_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Songs_Games_GameId1",
                        column: x => x.GameId1,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Songs_Viewers_Viewerid",
                        column: x => x.Viewerid,
                        principalTable: "Viewers",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "Viewers",
                columns: new[] { "id", "display", "name", "role", "subscribed", "type" },
                values: new object[,]
                {
                    { "100135110", "StreamElements", "streamelements", 3, false, "twitch" },
                    { "106313947", "TheStBlaine", "thestblaine", 1, false, "twitch" },
                    { "12640", "Credulus", "credulus", 3, false, "twitch" },
                    { "169517884", "AndyInTheFork", "andyinthefork", 3, false, "twitch" },
                    { "206992018", "NoBuddyIsPerfect", "nobuddyisperfect", 3, false, "twitch" },
                    { "228757727", "bryanthecoolcat", "bryanthecoolcat", 1, false, "twitch" },
                    { "264243225", "wizenedwizards", "wizenedwizards", 1, false, "twitch" },
                    { "401548213", "WildTomcat5", "wildtomcat5", 2, false, "twitch" },
                    { "43161910", "gamerjaym", "gamerjaym", 1, false, "twitch" },
                    { "498666678", "SaltSkeggur", "saltskeggur", 1, false, "twitch" },
                    { "51010749", "Bai_nin", "bai_nin", 1, false, "twitch" },
                    { "535004677", "ItReins", "itreins", 2, false, "twitch" },
                    { "557980940", "dcocol", "dcocol", 3, false, "twitch" },
                    { "579901532", "Milbrandt_2", "milbrandt_2", 1, false, "twitch" },
                    { "62053184", "pkn345", "pkn345", 0, false, "twitch" },
                    { "641794374", "hans154", "hans154", 1, false, "twitch" },
                    { "674951982", "ghe_di", "ghe_di", 1, false, "twitch" },
                    { "725622089", "Retisska", "retisska", 3, true, "twitch" },
                    { "734501812", "nobotisperfect", "nobotisperfect", 3, false, "twitch" },
                    { "756550096", "omos1", "omos1", 2, false, "twitch" },
                    { "785731504", "1_senior", "1_senior", 1, false, "twitch" },
                    { "79268687", "Karvaooppeli", "karvaooppeli", 1, false, "twitch" },
                    { "876112028", "NoAltIsPerfect", "noaltisperfect", 4, false, "twitch" },
                    { "884443102", "SheriffCroco", "sheriffcroco", 1, false, "twitch" },
                    { "955237329", "FrostyToolsDotCom", "frostytoolsdotcom", 3, false, "twitch" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Battles_RoundId",
                table: "Battles",
                column: "RoundNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_RoundId1",
                table: "Battles",
                column: "RoundId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Battles_RoundId2",
                table: "Battles",
                column: "RoundId2");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_Song1Id",
                table: "Battles",
                column: "Song1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_Song2Id",
                table: "Battles",
                column: "Song2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_WinnerId",
                table: "Battles",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_CurrentRoundId",
                table: "Games",
                column: "CurrentRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_GameId",
                table: "Rounds",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_GameId",
                table: "Songs",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_GameId1",
                table: "Songs",
                column: "GameId1");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_Viewerid",
                table: "Songs",
                column: "Viewerid");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Rounds_RoundId",
                table: "Battles",
                column: "RoundNumber",
                principalTable: "Rounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Rounds_RoundId1",
                table: "Battles",
                column: "RoundId1",
                principalTable: "Rounds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Rounds_RoundId2",
                table: "Battles",
                column: "RoundId2",
                principalTable: "Rounds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Songs_Song1Id",
                table: "Battles",
                column: "Song1Id",
                principalTable: "Songs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Songs_Song2Id",
                table: "Battles",
                column: "Song2Id",
                principalTable: "Songs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Songs_WinnerId",
                table: "Battles",
                column: "WinnerId",
                principalTable: "Songs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Rounds_CurrentRoundId",
                table: "Games",
                column: "CurrentRoundId",
                principalTable: "Rounds",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Rounds_CurrentRoundId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "Battles");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "Viewers");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
