using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAnotherLeaveType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8f3fe8df-fa03-4fae-9752-fa102410c5b8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee868370-9ea5-418e-b363-a411804467a6", "AQAAAAIAAYagAAAAEHyR3W/WXE+Br9J9+tTN5wC2+niOPJALzOIGvw+ieZbM7B7yZr8kquIVDHJVYzNXSw==", "31e72761-7ce7-4a1d-be88-3eb75cc53816" });

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "Name", "NumberOfDays" },
                values: new object[] { 51, "Holiday Leave", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8f3fe8df-fa03-4fae-9752-fa102410c5b8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "04ac8dc1-bf31-4fe7-92b3-325fb94fe40f", "AQAAAAIAAYagAAAAEI0Af53sAX+aHckpTusEQLjMcrM7WWF/B50h+PMKijydKnjrTt49iQz/GKjd0ziU0w==", "5371e2de-28ae-471a-b605-b1194f0a0222" });
        }
    }
}
