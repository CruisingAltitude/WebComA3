using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPortal.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedArticleModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Accounts_AuthorAccountId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_AuthorAccountId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "AuthorAccountId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "AuthourId",
                table: "Articles",
                newName: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Accounts_AuthorId",
                table: "Articles",
                column: "AuthorId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Accounts_AuthorId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Articles",
                newName: "AuthourId");

            migrationBuilder.AddColumn<int>(
                name: "AuthorAccountId",
                table: "Articles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AuthorAccountId",
                table: "Articles",
                column: "AuthorAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Accounts_AuthorAccountId",
                table: "Articles",
                column: "AuthorAccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId");
        }
    }
}
