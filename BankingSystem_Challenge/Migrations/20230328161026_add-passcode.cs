using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingSystem_Challenge.Migrations
{
    /// <inheritdoc />
    public partial class addpasscode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Passcode",
                table: "Accounts",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Passcode",
                table: "Accounts");
        }
    }
}
