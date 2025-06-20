using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditShowTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowTimes_Cinemas_CinemaId",
                table: "ShowTimes");

            migrationBuilder.DropIndex(
                name: "IX_ShowTimes_CinemaId",
                table: "ShowTimes");

            migrationBuilder.DropColumn(
                name: "CinemaId",
                table: "ShowTimes");

            migrationBuilder.CreateIndex(
                name: "IX_ShowTimes_Cinema_Id",
                table: "ShowTimes",
                column: "Cinema_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTimes_Cinemas_Cinema_Id",
                table: "ShowTimes",
                column: "Cinema_Id",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowTimes_Cinemas_Cinema_Id",
                table: "ShowTimes");

            migrationBuilder.DropIndex(
                name: "IX_ShowTimes_Cinema_Id",
                table: "ShowTimes");

            migrationBuilder.AddColumn<int>(
                name: "CinemaId",
                table: "ShowTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShowTimes_CinemaId",
                table: "ShowTimes",
                column: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTimes_Cinemas_CinemaId",
                table: "ShowTimes",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
