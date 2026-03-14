using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Frogobot.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGameModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameName = table.Column<string>(type: "TEXT", nullable: false),
                    BaseMaxPlayerCount = table.Column<uint>(type: "INTEGER", nullable: false),
                    AllowCustomLobbies = table.Column<bool>(type: "INTEGER", nullable: false),
                    MaxTeamCount = table.Column<uint>(type: "INTEGER", nullable: true),
                    MaxPlayersPerTeam = table.Column<uint>(type: "INTEGER", nullable: true),
                    SupportsModsForMorePlayers = table.Column<bool>(type: "INTEGER", nullable: false),
                    ModdedMaxPlayerCount = table.Column<uint>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "UserGames",
                columns: table => new
                {
                    DiscordUserId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    GameId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGames", x => new { x.DiscordUserId, x.GameId });
                    table.ForeignKey(
                        name: "FK_UserGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGames_Users_DiscordUserId",
                        column: x => x.DiscordUserId,
                        principalTable: "Users",
                        principalColumn: "DiscordUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGames_GameId",
                table: "UserGames",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGames");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
