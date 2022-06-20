using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Db.Migrations.Migrations
{
    public partial class FixedNullableTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RecModified",
                table: "Group",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 20, 16, 28, 58, 869, DateTimeKind.Utc).AddTicks(8370),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 6, 12, 20, 1, 36, 599, DateTimeKind.Utc).AddTicks(6812));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecCreated",
                table: "Group",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 20, 16, 28, 58, 869, DateTimeKind.Utc).AddTicks(8202),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 6, 12, 20, 1, 36, 599, DateTimeKind.Utc).AddTicks(6649));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Group",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RecModified",
                table: "Group",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 12, 20, 1, 36, 599, DateTimeKind.Utc).AddTicks(6812),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 6, 20, 16, 28, 58, 869, DateTimeKind.Utc).AddTicks(8370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecCreated",
                table: "Group",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 12, 20, 1, 36, 599, DateTimeKind.Utc).AddTicks(6649),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 6, 20, 16, 28, 58, 869, DateTimeKind.Utc).AddTicks(8202));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Group",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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
                value: "AQAAAAEAACcQAAAAEBO+EfuFeO1j+34xk4bXH/kxMam3hzOQPGQlEKJ3yFFqlYORrVz/L2/6mrWjTZI1zw==");
        }
    }
}
