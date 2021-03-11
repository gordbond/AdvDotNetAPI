using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvDotNetAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        /// <summary>
        /// MigrationBuilder that is used in the API, variables for Immunization class are being set to columns in a table
        /// </summary>
        /// <param name="migrationBuilder"> MigrationBuilder Object</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Immunizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficialName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    TradeName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LotNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Expiration = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    //Primary Key = Immunization id
                    table.PrimaryKey("PK_Immunizations", x => x.Id);
                });

           // Table created using variables for Organizations class are being set to columns 
            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    //Primary Key = Organization id
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            // Table created using variables for Patients class are being set to columns 
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    //Primary Key = Patients id
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            // Table created using variables for Providers class are being set to columns 
            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicenseNumber = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    //Primary Key = Providers id
                    table.PrimaryKey("PK_Providers", x => x.Id);
                });
        }

        /// <summary>
        /// Method Down that uses the MigrationBuilder to drop all tables
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Immunizations");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Providers");
        }
    }
}
