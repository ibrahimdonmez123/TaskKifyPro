using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskKifyPro.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NoteAddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TargetUser",
                table: "Notifications",
                newName: "TeamId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Notifications",
                newName: "TargetUser");

            migrationBuilder.AlterColumn<int>(
                name: "Title",
                table: "Notifications",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
