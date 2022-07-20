using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutboxPattern.Demo.Services.Ordering.Infrastructure.Migrations
{
    public partial class _04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "published",
                table: "outboxMessages",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "failedReason",
                table: "outboxMessages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "failedReason",
                table: "outboxMessages");

            migrationBuilder.AlterColumn<bool>(
                name: "published",
                table: "outboxMessages",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
