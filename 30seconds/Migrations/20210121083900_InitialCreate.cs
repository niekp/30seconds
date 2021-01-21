using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace _30seconds.Migrations {
	public partial class InitialCreate : Migration {
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateTable(
				name: "Wordlist",
				columns: table => new {
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Title = table.Column<string>(type: "TEXT", nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_Wordlist", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Room",
				columns: table => new {
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Name = table.Column<string>(type: "TEXT", nullable: true),
					IdWordlist = table.Column<int>(type: "INTEGER", nullable: false),
					AmountOfSeconds = table.Column<int>(type: "INTEGER", nullable: false),
					Created = table.Column<DateTime>(type: "TEXT", nullable: false),
					LastPing = table.Column<DateTime>(type: "TEXT", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Room", x => x.Id);
					table.ForeignKey(
						name: "FK_Room_Wordlist_IdWordlist",
						column: x => x.IdWordlist,
						principalTable: "Wordlist",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Game",
				columns: table => new {
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					IdRoom = table.Column<int>(type: "INTEGER", nullable: false),
					Start = table.Column<DateTime>(type: "TEXT", nullable: false),
					User = table.Column<string>(type: "TEXT", nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_Game", x => x.Id);
					table.ForeignKey(
						name: "FK_Game_Room_IdRoom",
						column: x => x.IdRoom,
						principalTable: "Room",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Word",
				columns: table => new {
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					IdWordlist = table.Column<int>(type: "INTEGER", nullable: false),
					Text = table.Column<string>(type: "TEXT", nullable: true),
					GameId = table.Column<int>(type: "INTEGER", nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_Word", x => x.Id);
					table.ForeignKey(
						name: "FK_Word_Game_GameId",
						column: x => x.GameId,
						principalTable: "Game",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Word_Wordlist_IdWordlist",
						column: x => x.IdWordlist,
						principalTable: "Wordlist",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Game_IdRoom",
				table: "Game",
				column: "IdRoom");

			migrationBuilder.CreateIndex(
				name: "IX_Room_IdWordlist",
				table: "Room",
				column: "IdWordlist");

			migrationBuilder.CreateIndex(
				name: "IX_Word_GameId",
				table: "Word",
				column: "GameId");

			migrationBuilder.CreateIndex(
				name: "IX_Word_IdWordlist",
				table: "Word",
				column: "IdWordlist");
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "Word");

			migrationBuilder.DropTable(
				name: "Game");

			migrationBuilder.DropTable(
				name: "Room");

			migrationBuilder.DropTable(
				name: "Wordlist");
		}
	}
}
