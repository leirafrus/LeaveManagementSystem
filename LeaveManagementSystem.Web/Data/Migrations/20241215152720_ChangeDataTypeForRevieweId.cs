using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDataTypeForRevieweId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_ReviewerId1",
                table: "LeaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequests_ReviewerId1",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "ReviewerId1",
                table: "LeaveRequests");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewerId",
                table: "LeaveRequests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8f3fe8df-fa03-4fae-9752-fa102410c5b8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "863f4193-907d-4e36-b2ff-64ef699ecd03", "AQAAAAIAAYagAAAAEIIhEnAcidR7IOtIfS9EpTm8UuATUps+pqi5Abv/krhvGwb+PDneNFy3Hk6YaD92ZQ==", "ba6f0065-9c68-4467-b68d-23dc338924c8" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_ReviewerId",
                table: "LeaveRequests",
                column: "ReviewerId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_ReviewerId",
                table: "LeaveRequests",
                column: "ReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_ReviewerId",
                table: "LeaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequests_ReviewerId",
                table: "LeaveRequests");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewerId",
                table: "LeaveRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewerId1",
                table: "LeaveRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8f3fe8df-fa03-4fae-9752-fa102410c5b8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc6091b8-8644-49f7-bc3b-b3cfb1dcfb64", "AQAAAAIAAYagAAAAEL+o4px2KBX5XGMOthR027pvMAaRrmhKMg0QIsDUe4dhgw0RWFf+mAC72PIevNQA+A==", "4b96a412-0329-4380-b2c9-859d20c24575" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_ReviewerId1",
                table: "LeaveRequests",
                column: "ReviewerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_ReviewerId1",
                table: "LeaveRequests",
                column: "ReviewerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
