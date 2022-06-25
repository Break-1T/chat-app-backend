using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Db.Migrations.Migrations
{
    public partial class AddChatMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RecModified",
                table: "Group",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now() at time zone 'utc'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 6, 20, 16, 28, 58, 869, DateTimeKind.Utc).AddTicks(8370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecCreated",
                table: "Group",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now() at time zone 'utc'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 6, 20, 16, 28, 58, 869, DateTimeKind.Utc).AddTicks(8202));

            migrationBuilder.AlterColumn<byte[]>(
                name: "Photo",
                table: "AspNetUsers",
                type: "bytea",
                nullable: true,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true,
                oldDefaultValue: new byte[0]);

            migrationBuilder.CreateTable(
                name: "ChatMessage",
                columns: table => new
                {
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: true),
                    RecCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessage", x => x.ChatId);
                    table.ForeignKey(
                        name: "FK_ChatMessage_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessage_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("edc610ea-54e0-4c8f-87d1-103b4f341b6b"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEP8fTmH8BVCzBNSW6pOuZWLr3lSxy26z6nNBgDAbeTSyjhhosUMdyYtcvcQ2gKERXQ==");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_GroupId_Message",
                table: "ChatMessage",
                columns: new[] { "GroupId", "Message" });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_UserId",
                table: "ChatMessage",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessage");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecModified",
                table: "Group",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 20, 16, 28, 58, 869, DateTimeKind.Utc).AddTicks(8370),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now() at time zone 'utc'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecCreated",
                table: "Group",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 20, 16, 28, 58, 869, DateTimeKind.Utc).AddTicks(8202),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now() at time zone 'utc'");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Photo",
                table: "AspNetUsers",
                type: "bytea",
                nullable: true,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true,
                oldDefaultValue: new byte[0]);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("edc610ea-54e0-4c8f-87d1-103b4f341b6b"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEJcHO5DqtIMsMj3646tWb/N6XKaFfOMasxuryr4eptla3rNCCyaRjJ9tON4yzHW/hg==");
        }
    }
}
