using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTracker.Migrations
{
    public partial class InitializeDatabaseModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionsData_Actions_ActionId1",
                table: "ActionsData");

            migrationBuilder.DropIndex(
                name: "IX_ActionsData_ActionId1",
                table: "ActionsData");

            migrationBuilder.DropColumn(
                name: "ActionId1",
                table: "ActionsData");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionsData_Actions_ActionId",
                table: "ActionsData");

            migrationBuilder.DropIndex(
                name: "IX_ActionsData_ActionId",
                table: "ActionsData");

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

            migrationBuilder.CreateIndex(
                name: "IX_ActionsData_ActionId1",
                table: "ActionsData",
                column: "ActionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionsData_Actions_ActionId1",
                table: "ActionsData",
                column: "ActionId1",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
