using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatingApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LeagueIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Leagues_LeagueId",
                table: "Players");

            migrationBuilder.AlterColumn<Guid>(
                name: "LeagueId",
                table: "Players",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Leagues_LeagueId",
                table: "Players",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Leagues_LeagueId",
                table: "Players");

            migrationBuilder.AlterColumn<Guid>(
                name: "LeagueId",
                table: "Players",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Leagues_LeagueId",
                table: "Players",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
