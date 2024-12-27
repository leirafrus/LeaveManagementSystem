using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedLeaveRequestStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_EmployeeId1",
                table: "LeaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequests_EmployeeId1",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "LeaveRequests");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "LeaveRequests",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8f3fe8df-fa03-4fae-9752-fa102410c5b8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc6091b8-8644-49f7-bc3b-b3cfb1dcfb64", "AQAAAAIAAYagAAAAEL+o4px2KBX5XGMOthR027pvMAaRrmhKMg0QIsDUe4dhgw0RWFf+mAC72PIevNQA+A==", "4b96a412-0329-4380-b2c9-859d20c24575" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_EmployeeId",
                table: "LeaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequests_EmployeeId",
                table: "LeaveRequests");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "LeaveRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId1",
                table: "LeaveRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8f3fe8df-fa03-4fae-9752-fa102410c5b8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e79ea27-6cea-43c6-856a-3b17f713b6d2", "AQAAAAIAAYagAAAAEG12urkY4yqBI/2hx47OISn+sDR9wverYleqVe4t9AVCrB4ym7qYqq6gEmLIpgCXWQ==", "89989746-bbf7-4892-af0f-c88695f69242" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_EmployeeId1",
                table: "LeaveRequests",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_EmployeeId1",
                table: "LeaveRequests",
                column: "EmployeeId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
