using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SINPRO.Migrations
{
    public partial class dtup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { new DateTime(2022, 1, 15, 17, 2, 2, 329, DateTimeKind.Local).AddTicks(6209), new DateTime(2022, 1, 15, 17, 2, 2, 329, DateTimeKind.Local).AddTicks(6460) });

            migrationBuilder.UpdateData(
                table: "mRole",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "inserted", "updated" },
                values: new object[] { new DateTime(2022, 1, 15, 17, 2, 2, 329, DateTimeKind.Local).AddTicks(6949), new DateTime(2022, 1, 15, 17, 2, 2, 329, DateTimeKind.Local).AddTicks(6953) });

            migrationBuilder.UpdateData(
                table: "mUser",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "inserted", "updated" },
                values: new object[] { new DateTime(2022, 1, 15, 17, 2, 2, 330, DateTimeKind.Local).AddTicks(6830), new DateTime(2022, 1, 15, 17, 2, 2, 330, DateTimeKind.Local).AddTicks(7045) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { new DateTime(2022, 1, 13, 17, 45, 36, 51, DateTimeKind.Local).AddTicks(3904), new DateTime(2022, 1, 13, 17, 45, 36, 52, DateTimeKind.Local).AddTicks(1598) });

            migrationBuilder.UpdateData(
                table: "mRole",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "inserted", "updated" },
                values: new object[] { new DateTime(2022, 1, 13, 17, 45, 36, 52, DateTimeKind.Local).AddTicks(2538), new DateTime(2022, 1, 13, 17, 45, 36, 52, DateTimeKind.Local).AddTicks(2545) });

            migrationBuilder.UpdateData(
                table: "mUser",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "inserted", "updated" },
                values: new object[] { new DateTime(2022, 1, 13, 17, 45, 36, 53, DateTimeKind.Local).AddTicks(6712), new DateTime(2022, 1, 13, 17, 45, 36, 53, DateTimeKind.Local).AddTicks(7054) });
        }
    }
}
