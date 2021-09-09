using Microsoft.EntityFrameworkCore.Migrations;

namespace MyNotes.DataAccess.Migrations
{
    public partial class FileExplanationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "path",
                schema: "public",
                table: "file_entity",
                newName: "saved_file_name");

            migrationBuilder.AlterColumn<string>(
                name: "message",
                schema: "public",
                table: "note",
                type: "character varying(1500)",
                maxLength: 1500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                schema: "public",
                table: "file_entity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "explanation",
                schema: "public",
                table: "file_entity",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                schema: "public",
                table: "file_entity");

            migrationBuilder.DropColumn(
                name: "explanation",
                schema: "public",
                table: "file_entity");

            migrationBuilder.RenameColumn(
                name: "saved_file_name",
                schema: "public",
                table: "file_entity",
                newName: "path");

            migrationBuilder.AlterColumn<string>(
                name: "message",
                schema: "public",
                table: "note",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1500)",
                oldMaxLength: 1500,
                oldNullable: true);
        }
    }
}
