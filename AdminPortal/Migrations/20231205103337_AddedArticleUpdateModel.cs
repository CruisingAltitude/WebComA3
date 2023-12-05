using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddedArticleUpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleUpdates",
                columns: table => new
                {
                    UpdateId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArticleId = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdaterId = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateTimeUTC = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PreviousField = table.Column<string>(type: "TEXT", nullable: false),
                    PreviousValue = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleUpdates", x => x.UpdateId);
                    table.ForeignKey(
                        name: "FK_ArticleUpdates_Accounts_UpdaterId",
                        column: x => x.UpdaterId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleUpdates_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleUpdates_ArticleId",
                table: "ArticleUpdates",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleUpdates_UpdaterId",
                table: "ArticleUpdates",
                column: "UpdaterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleUpdates");
        }
    }
}
