using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FNSC.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRoundFromBattle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battles_Rounds_RoundId1",
                table: "Battles");

            migrationBuilder.DropForeignKey(
                name: "FK_Battles_Rounds_RoundId2",
                table: "Battles");

            migrationBuilder.DropForeignKey(
                name: "FK_Battles_Rounds_RoundNumber",
                table: "Battles");

            migrationBuilder.DropIndex(
                name: "IX_Battles_RoundId1",
                table: "Battles");

        
            migrationBuilder.DropColumn(
                name: "RoundId1",
                table: "Battles");

            migrationBuilder.RenameColumn(
                name: "RoundId2",
                table: "Battles",
                newName: "RoundNumber");

          
            migrationBuilder.AddColumn<string>(
                name: "CurrentBattleId",
                table: "Rounds",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_CurrentBattleId",
                table: "Rounds",
                column: "CurrentBattleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Rounds_RoundId",
                table: "Battles",
                column: "RoundId",
                principalTable: "Rounds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_Battles_CurrentBattleId",
                table: "Rounds",
                column: "CurrentBattleId",
                principalTable: "Battles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battles_Rounds_RoundId",
                table: "Battles");

            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Battles_CurrentBattleId",
                table: "Rounds");

            migrationBuilder.DropIndex(
                name: "IX_Rounds_CurrentBattleId",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "CurrentBattleId",
                table: "Rounds");

            migrationBuilder.RenameColumn(
                name: "RoundId",
                table: "Battles",
                newName: "RoundId2");

            migrationBuilder.RenameIndex(
                name: "IX_Battles_RoundId",
                table: "Battles",
                newName: "IX_Battles_RoundId2");

            migrationBuilder.AddColumn<int>(
                name: "RoundId1",
                table: "Battles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Battles_RoundId1",
                table: "Battles",
                column: "RoundId1",
                unique: true);
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
                name: "FK_Battles_Rounds_RoundNumber",
                table: "Battles",
                column: "RoundNumber",
                principalTable: "Rounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
