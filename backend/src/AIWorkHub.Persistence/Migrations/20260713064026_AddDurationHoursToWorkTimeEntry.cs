using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIWorkHub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDurationHoursToWorkTimeEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DurationHours",
                table: "WorkTimeEntries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationHours",
                table: "WorkTimeEntries");
        }
    }
}
