using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookish.Migrations
{
    /// <inheritdoc />
    public partial class AddBookCatalogue_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Catalogue_id",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_Catalogue_id",
                table: "Books",
                column: "Catalogue_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Catalogue_Catalogue_id",
                table: "Books",
                column: "Catalogue_id",
                principalTable: "Catalogue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Catalogue_Catalogue_id",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_Catalogue_id",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Catalogue_id",
                table: "Books");
        }
    }
}
