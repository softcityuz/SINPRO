using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SINPRO.Migrations
{
    public partial class addtext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "textDescription",
                table: "mMatch",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "mRole",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "inserted", "updated" },
                values: new object[] { new DateTime(2022, 1, 17, 22, 15, 55, 213, DateTimeKind.Local).AddTicks(9272), new DateTime(2022, 1, 17, 22, 15, 55, 213, DateTimeKind.Local).AddTicks(9530) });

            migrationBuilder.UpdateData(
                table: "mRole",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "inserted", "updated" },
                values: new object[] { new DateTime(2022, 1, 17, 22, 15, 55, 214, DateTimeKind.Local).AddTicks(48), new DateTime(2022, 1, 17, 22, 15, 55, 214, DateTimeKind.Local).AddTicks(52) });

            migrationBuilder.UpdateData(
                table: "mUser",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "inserted", "updated" },
                values: new object[] { new DateTime(2022, 1, 17, 22, 15, 55, 214, DateTimeKind.Local).AddTicks(9324), new DateTime(2022, 1, 17, 22, 15, 55, 214, DateTimeKind.Local).AddTicks(9556) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "textDescription",
                table: "mMatch");

            migrationBuilder.UpdateData(
                table: "mRole",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "inserted", "updated" },
                values: new object[] { new DateTime(2022, 1, 16, 3, 3, 45, 134, DateTimeKind.Local).AddTicks(1977), new DateTime(2022, 1, 16, 3, 3, 45, 134, DateTimeKind.Local).AddTicks(2234) });

            migrationBuilder.UpdateData(
                table: "mRole",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "inserted", "updated" },
                values: new object[] { new DateTime(2022, 1, 16, 3, 3, 45, 134, DateTimeKind.Local).AddTicks(2747), new DateTime(2022, 1, 16, 3, 3, 45, 134, DateTimeKind.Local).AddTicks(2751) });

            migrationBuilder.UpdateData(
                table: "mUser",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "inserted", "updated" },
                values: new object[] { new DateTime(2022, 1, 16, 3, 3, 45, 135, DateTimeKind.Local).AddTicks(3625), new DateTime(2022, 1, 16, 3, 3, 45, 135, DateTimeKind.Local).AddTicks(3854) });
        }
    }
}
