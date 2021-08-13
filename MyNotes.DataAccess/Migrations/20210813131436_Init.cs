using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyNotes.DataAccess.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "global_right",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    access_type = table.Column<int>(type: "integer", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    edit_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_deleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_global_right", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "local_right",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    allowed_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    access_type = table.Column<int>(type: "integer", nullable: false),
                    entity_type = table.Column<int>(type: "integer", nullable: false),
                    entity_id = table.Column<Guid>(type: "uuid", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    edit_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_deleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_local_right", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "topic",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    edit_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_deleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_topic", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "paragraph",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    topic_id = table.Column<Guid>(type: "uuid", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    edit_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_deleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paragraph", x => x.id);
                    table.ForeignKey(
                        name: "FK_paragraph_topic_topic_id",
                        column: x => x.topic_id,
                        principalSchema: "public",
                        principalTable: "topic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comment",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    paragraph_id = table.Column<Guid>(type: "uuid", nullable: false),
                    message = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    edit_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_deleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment", x => x.id);
                    table.ForeignKey(
                        name: "FK_comment_paragraph_paragraph_id",
                        column: x => x.paragraph_id,
                        principalSchema: "public",
                        principalTable: "paragraph",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "note",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    message = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    paragraph_id = table.Column<Guid>(type: "uuid", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    edit_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_deleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_note", x => x.id);
                    table.ForeignKey(
                        name: "FK_note_paragraph_paragraph_id",
                        column: x => x.paragraph_id,
                        principalSchema: "public",
                        principalTable: "paragraph",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fileEntity_map",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    note_id = table.Column<Guid>(type: "uuid", nullable: false),
                    path = table.Column<string>(type: "text", nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    edit_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_deleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fileEntity_map", x => x.id);
                    table.ForeignKey(
                        name: "FK_fileEntity_map_note_note_id",
                        column: x => x.note_id,
                        principalSchema: "public",
                        principalTable: "note",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comment_paragraph_id",
                schema: "public",
                table: "comment",
                column: "paragraph_id");

            migrationBuilder.CreateIndex(
                name: "IX_fileEntity_map_note_id",
                schema: "public",
                table: "fileEntity_map",
                column: "note_id");

            migrationBuilder.CreateIndex(
                name: "IX_note_paragraph_id",
                schema: "public",
                table: "note",
                column: "paragraph_id");

            migrationBuilder.CreateIndex(
                name: "IX_paragraph_topic_id",
                schema: "public",
                table: "paragraph",
                column: "topic_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comment",
                schema: "public");

            migrationBuilder.DropTable(
                name: "fileEntity_map",
                schema: "public");

            migrationBuilder.DropTable(
                name: "global_right",
                schema: "public");

            migrationBuilder.DropTable(
                name: "local_right",
                schema: "public");

            migrationBuilder.DropTable(
                name: "note",
                schema: "public");

            migrationBuilder.DropTable(
                name: "paragraph",
                schema: "public");

            migrationBuilder.DropTable(
                name: "topic",
                schema: "public");
        }
    }
}
