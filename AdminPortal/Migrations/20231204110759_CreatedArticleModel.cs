using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPortal.Migrations
{
    /// <inheritdoc />
    public partial class CreatedArticleModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthourId = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthorAccountId = table.Column<int>(type: "INTEGER", nullable: true),
                    ArticleTitle = table.Column<string>(type: "TEXT", nullable: false),
                    ArticleBody = table.Column<string>(type: "TEXT", nullable: true),
                    CreationTimeUTC = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PublishTimeUTC = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Hidden = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Articles_Accounts_AuthorAccountId",
                        column: x => x.AuthorAccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AuthorAccountId",
                table: "Articles",
                column: "AuthorAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}
