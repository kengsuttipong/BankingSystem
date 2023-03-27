using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingSystem_Challenge.Migrations
{
    /// <inheritdoc />
    public partial class firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Accounts",
            //    columns: table => new
            //    {
            //        account_id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        customer_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        iban = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        account_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
            //        balance = table.Column<double>(type: "float", nullable: true),
            //        date_created = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Accounts", x => x.account_id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AccountType",
            //    columns: table => new
            //    {
            //        AccountTypeID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        AccountTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AccountType", x => x.AccountTypeID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Country",
            //    columns: table => new
            //    {
            //        CountryID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CountryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        CountryCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Country", x => x.CountryID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Customers",
            //    columns: table => new
            //    {
            //        customer_id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        phone_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        date_of_birth = table.Column<DateTime>(type: "date", nullable: true),
            //        address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Customer", x => x.customer_id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Transactions",
            //    columns: table => new
            //    {
            //        transaction_id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        from_account_id = table.Column<int>(type: "int", nullable: true),
            //        to_iban = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        amount = table.Column<double>(type: "float", nullable: true),
            //        currency = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
            //        transaction_fee = table.Column<double>(type: "float", nullable: true),
            //        date_executed = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Transactions", x => x.transaction_id);
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountType");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
