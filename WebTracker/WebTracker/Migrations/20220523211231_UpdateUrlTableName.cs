using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTracker.Migrations
{
    public partial class UpdateUrlTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Url_UrlId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Url_Flows_FlowId",
                table: "Url");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Url",
                table: "Url");

            migrationBuilder.RenameTable(
                name: "Url",
                newName: "Urls");

            migrationBuilder.RenameIndex(
                name: "IX_Url_FlowId",
                table: "Urls",
                newName: "IX_Urls_FlowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Urls",
                table: "Urls",
                column: "UrlId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_WebsiteId",
                table: "Users",
                column: "WebsiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Urls_UrlId",
                table: "Actions",
                column: "UrlId",
                principalTable: "Urls",
                principalColumn: "UrlId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Urls_Flows_FlowId",
                table: "Urls",
                column: "FlowId",
                principalTable: "Flows",
                principalColumn: "FlowId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Websites_WebsiteId",
                table: "Users",
                column: "WebsiteId",
                principalTable: "Websites",
                principalColumn: "WebsiteId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Urls_UrlId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Urls_Flows_FlowId",
                table: "Urls");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Websites_WebsiteId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_WebsiteId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Urls",
                table: "Urls");

            migrationBuilder.RenameTable(
                name: "Urls",
                newName: "Url");

            migrationBuilder.RenameIndex(
                name: "IX_Urls_FlowId",
                table: "Url",
                newName: "IX_Url_FlowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Url",
                table: "Url",
                column: "UrlId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Url_UrlId",
                table: "Actions",
                column: "UrlId",
                principalTable: "Url",
                principalColumn: "UrlId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Url_Flows_FlowId",
                table: "Url",
                column: "FlowId",
                principalTable: "Flows",
                principalColumn: "FlowId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
