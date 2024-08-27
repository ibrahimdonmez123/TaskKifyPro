using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskKifyPro.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class secorndcorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroups",
                table: "UserGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAndUserGroups",
                table: "UserAndUserGroups");

            migrationBuilder.RenameTable(
                name: "UserGroups",
                newName: "Duties");

            migrationBuilder.RenameTable(
                name: "UserAndUserGroups",
                newName: "UserDuties");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Duties",
                table: "Duties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDuties",
                table: "UserDuties",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDuties",
                table: "UserDuties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Duties",
                table: "Duties");

            migrationBuilder.RenameTable(
                name: "UserDuties",
                newName: "UserAndUserGroups");

            migrationBuilder.RenameTable(
                name: "Duties",
                newName: "UserGroups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAndUserGroups",
                table: "UserAndUserGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroups",
                table: "UserGroups",
                column: "Id");
        }
    }
}
