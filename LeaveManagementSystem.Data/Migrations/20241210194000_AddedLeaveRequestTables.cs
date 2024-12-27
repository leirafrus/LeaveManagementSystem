using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLeaveRequestTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveRequestStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequestStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    LeaveRequestStatusId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ReviewerId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReviewerId = table.Column<int>(type: "int", nullable: true),
                    RequestComments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_AspNetUsers_EmployeeId1",
                        column: x => x.EmployeeId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveRequests_AspNetUsers_ReviewerId1",
                        column: x => x.ReviewerId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveRequestStatuses_LeaveRequestStatusId",
                        column: x => x.LeaveRequestStatusId,
                        principalTable: "LeaveRequestStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8f3fe8df-fa03-4fae-9752-fa102410c5b8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e79ea27-6cea-43c6-856a-3b17f713b6d2", "AQAAAAIAAYagAAAAEG12urkY4yqBI/2hx47OISn+sDR9wverYleqVe4t9AVCrB4ym7qYqq6gEmLIpgCXWQ==", "89989746-bbf7-4892-af0f-c88695f69242" });

            migrationBuilder.InsertData(
                table: "LeaveRequestStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Approved" },
                    { 3, "Declined" },
                    { 4, "Canceled" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_EmployeeId1",
                table: "LeaveRequests",
                column: "EmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveRequestStatusId",
                table: "LeaveRequests",
                column: "LeaveRequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_ReviewerId1",
                table: "LeaveRequests",
                column: "ReviewerId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.DropTable(
                name: "LeaveRequestStatuses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8f3fe8df-fa03-4fae-9752-fa102410c5b8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7888d78-bde2-4b53-9e38-eae0e4ed4908", "AQAAAAIAAYagAAAAEIMp/vlPbrBio0nAhXR2GyGp1kKMbgzwCui2b6MZg8Tb6cq+/7E9XtzCP3TO8H4hWQ==", "032e3617-c2fe-4bb9-8876-167a7da6f1d3" });
        }
    }
}
