using Microsoft.EntityFrameworkCore.Migrations;

namespace MyNotes.DataAccess.Migrations
{
    public partial class MessageColoumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "message",
                schema: "public",
                table: "paragraph",
                type: "character varying(5000)",
                maxLength: 5000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "message",
                schema: "public",
                table: "paragraph");
        }
    }
}
