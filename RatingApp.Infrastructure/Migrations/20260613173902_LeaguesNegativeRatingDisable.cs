using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatingApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LeaguesNegativeRatingDisable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "RequiredRating",
                table: "Leagues",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RequiredRating",
                table: "Leagues",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
