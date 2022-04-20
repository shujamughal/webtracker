using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTracker.Migrations
{
    public partial class UserToWebRelationUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Websites_WebId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_WebId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VisitCount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WebId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "ActionsData",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Websites",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisitCount",
                table: "Websites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ActionId",
                table: "ActionsData",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Websites_UserId",
                table: "Websites",
                column: "UserId");

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
                name: "FK_Websites_Users_UserId",
                table: "Websites",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionsData_Actions_ActionId",
                table: "ActionsData");

            migrationBuilder.DropForeignKey(
                name: "FK_Websites_Users_UserId",
                table: "Websites");

            migrationBuilder.DropIndex(
                name: "IX_Websites_UserId",
                table: "Websites");

            migrationBuilder.DropIndex(
                name: "IX_ActionsData_ActionId",
                table: "ActionsData");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Websites");

            migrationBuilder.DropColumn(
                name: "VisitCount",
                table: "Websites");

            migrationBuilder.DropColumn(
                name: "ActionId",
                table: "ActionsData");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ActionsData",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "VisitCount",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WebId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_WebId",
                table: "Users",
                column: "WebId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Websites_WebId",
                table: "Users",
                column: "WebId",
                principalTable: "Websites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
