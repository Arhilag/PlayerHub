using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayerHub.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    device_id = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    nickname = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    level = table.Column<int>(type: "integer", nullable: false),
                    coins = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_seen_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    is_banned = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_players", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "analytics_events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    player_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    parameters = table.Column<string>(type: "jsonb", nullable: false),
                    client_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    server_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    session_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_analytics_events", x => x.id);
                    table.ForeignKey(
                        name: "fk_analytics_events_players_player_id",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_analytics_events_name_server_time",
                table: "analytics_events",
                columns: new[] { "name", "server_time" });

            migrationBuilder.CreateIndex(
                name: "ix_analytics_events_player_id_server_time",
                table: "analytics_events",
                columns: new[] { "player_id", "server_time" });

            migrationBuilder.CreateIndex(
                name: "ix_analytics_events_session_id",
                table: "analytics_events",
                column: "session_id");

            migrationBuilder.CreateIndex(
                name: "ix_players_created_at",
                table: "players",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "ix_players_device_id",
                table: "players",
                column: "device_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_players_last_seen_at",
                table: "players",
                column: "last_seen_at");

            migrationBuilder.CreateIndex(
                name: "ix_players_nickname",
                table: "players",
                column: "nickname");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "analytics_events");

            migrationBuilder.DropTable(
                name: "players");
        }
    }
}
