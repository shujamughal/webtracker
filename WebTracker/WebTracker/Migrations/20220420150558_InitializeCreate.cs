using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTracker.Migrations
{
    public partial class InitializeCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionsData_Actions_ActionId",
                table: "ActionsData");

            migrationBuilder.DropForeignKey(
                name: "FK_Url_Flows_FlowId",
                table: "Url");

            migrationBuilder.DropForeignKey(
                name: "FK_Websites_Users_UserId",
                table: "Websites");

            migrationBuilder.DropIndex(
                name: "IX_ActionsData_ActionId",
                table: "ActionsData");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Websites",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FlowId",
                table: "Url",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WebsiteId",
                table: "Flows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ActionId",
                table: "ActionsData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActionId1",
                table: "ActionsData",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UrlId",
                table: "Actions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Flows_WebsiteId",
                table: "Flows",
                column: "WebsiteId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionsData_ActionId1",
                table: "ActionsData",
                column: "ActionId1");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_UrlId",
                table: "Actions",
                column: "UrlId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Url_UrlId",
                table: "Actions",
                column: "UrlId",
                principalTable: "Url",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActionsData_Actions_ActionId1",
                table: "ActionsData",
                column: "ActionId1",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flows_Websites_WebsiteId",
                table: "Flows",
                column: "WebsiteId",
                principalTable: "Websites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Url_Flows_FlowId",
                table: "Url",
                column: "FlowId",
                principalTable: "Flows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Websites_Users_UserId",
                table: "Websites",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Url_UrlId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_ActionsData_Actions_ActionId1",
                table: "ActionsData");

            migrationBuilder.DropForeignKey(
                name: "FK_Flows_Websites_WebsiteId",
                table: "Flows");

            migrationBuilder.DropForeignKey(
                name: "FK_Url_Flows_FlowId",
                table: "Url");

            migrationBuilder.DropForeignKey(
                name: "FK_Websites_Users_UserId",
                table: "Websites");

            migrationBuilder.DropIndex(
                name: "IX_Flows_WebsiteId",
                table: "Flows");

            migrationBuilder.DropIndex(
                name: "IX_ActionsData_ActionId1",
                table: "ActionsData");

            migrationBuilder.DropIndex(
                name: "IX_Actions_UrlId",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "WebsiteId",
                table: "Flows");

            migrationBuilder.DropColumn(
                name: "ActionId1",
                table: "ActionsData");

            migrationBuilder.DropColumn(
                name: "UrlId",
                table: "Actions");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Websites",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FlowId",
                table: "Url",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ActionId",
                table: "ActionsData",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ActionsData_ActionId",
                table: "ActionsData",
                column: "ActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionsData_Actions_ActionId",
                table: "ActionsData",
                column: "ActionId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Url_Flows_FlowId",
                table: "Url",
                column: "FlowId",
                principalTable: "Flows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Websites_Users_UserId",
                table: "Websites",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
