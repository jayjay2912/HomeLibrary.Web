using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gemstone.HomeLibrary.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReadBookModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ReadBooks_BookId",
                table: "ReadBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadBooks_UserId",
                table: "ReadBooks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReadBooks_Books_BookId",
                table: "ReadBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadBooks_Users_UserId",
                table: "ReadBooks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReadBooks_Books_BookId",
                table: "ReadBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadBooks_Users_UserId",
                table: "ReadBooks");

            migrationBuilder.DropIndex(
                name: "IX_ReadBooks_BookId",
                table: "ReadBooks");

            migrationBuilder.DropIndex(
                name: "IX_ReadBooks_UserId",
                table: "ReadBooks");
        }
    }
}
