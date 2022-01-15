using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SINPRO.Migrations
{
    public partial class statusdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "statusDate",
                table: "mUser",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "statusDate",
                table: "mUser");

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
    }
}
