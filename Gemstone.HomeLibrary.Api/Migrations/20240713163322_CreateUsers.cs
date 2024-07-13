using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gemstone.HomeLibrary.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Users (Id, Name) VALUES ('FCECA79F-B635-4733-9F42-04F0DFAA69F6', 'Gemma'), ('A6AAA168-E033-4992-8B7C-317671599573', 'Jay')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
