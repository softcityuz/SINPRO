using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SINPRO.Migrations
{
    public partial class timezones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "matchTime",
                table: "mMatch",
                type: "time(6)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "mRole",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "inserted", "updated" },
                values: new object[] { new DateTime(2022, 1, 17, 22, 23, 46, 270, DateTimeKind.Local).AddTicks(7863), new DateTime(2022, 1, 17, 22, 23, 46, 270, DateTimeKind.Local).AddTicks(8118) });

            migrationBuilder.UpdateData(
                table: "mRole",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "inserted", "updated" },
                values: new object[] { new DateTime(2022, 1, 17, 22, 23, 46, 270, DateTimeKind.Local).AddTicks(8629), new DateTime(2022, 1, 17, 22, 23, 46, 270, DateTimeKind.Local).AddTicks(8633) });

            migrationBuilder.UpdateData(
                table: "mUser",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "inserted", "updated" },
                values: new object[] { new DateTime(2022, 1, 17, 22, 23, 46, 271, DateTimeKind.Local).AddTicks(7870), new DateTime(2022, 1, 17, 22, 23, 46, 271, DateTimeKind.Local).AddTicks(8098) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "matchTime",
                table: "mMatch",
                type: "int",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)");

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
    }
}
