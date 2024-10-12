using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeChat.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Users_AddSomeColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailOTP",
                table: "Users",
                type: "character varying(6)",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProcessId",
                table: "Users",
                type: "text",
                nullable: true,
                defaultValue: "256");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailOTP",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProcessId",
                table: "Users");
        }
    }
}
