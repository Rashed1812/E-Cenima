using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Timings_timingId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_timingId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "timingId",
                table: "Tickets");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Timing_Id",
                table: "Tickets",
                column: "Timing_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Timings_Timing_Id",
                table: "Tickets",
                column: "Timing_Id",
                principalTable: "Timings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Timings_Timing_Id",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_Timing_Id",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "timingId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_timingId",
                table: "Tickets",
                column: "timingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Timings_timingId",
                table: "Tickets",
                column: "timingId",
                principalTable: "Timings",
                principalColumn: "Id");
        }
    }
}
