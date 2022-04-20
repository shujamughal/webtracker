using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTracker.Migrations
{
    public partial class WebsiteNameUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Websites_WebsiteId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "WebsiteId",
                table: "Users",
                newName: "WebId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_WebsiteId",
                table: "Users",
                newName: "IX_Users_WebId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Websites_WebId",
                table: "Users",
                column: "WebId",
                principalTable: "Websites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Websites_WebId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "WebId",
                table: "Users",
                newName: "WebsiteId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_WebId",
                table: "Users",
                newName: "IX_Users_WebsiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Websites_WebsiteId",
                table: "Users",
                column: "WebsiteId",
                principalTable: "Websites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
