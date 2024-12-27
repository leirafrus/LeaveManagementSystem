using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultRolesAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestTypes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45fbb2f4-87fa-4605-9c5f-912fa126fc44", null, "Employee", "EMPLOYEE" },
                    { "e62ab376-0761-4fb6-9a45-270084291888", null, "Administrator", "ADMINISTRATOR" },
                    { "f6f3a032-cb87-4620-94f4-ed3ce3821f7a", null, "Supervisor", "SUPERVISOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8f3fe8df-fa03-4fae-9752-fa102410c5b8", 0, "04ac8dc1-bf31-4fe7-92b3-325fb94fe40f", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEI0Af53sAX+aHckpTusEQLjMcrM7WWF/B50h+PMKijydKnjrTt49iQz/GKjd0ziU0w==", null, false, "5371e2de-28ae-471a-b605-b1194f0a0222", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e62ab376-0761-4fb6-9a45-270084291888", "8f3fe8df-fa03-4fae-9752-fa102410c5b8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45fbb2f4-87fa-4605-9c5f-912fa126fc44");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6f3a032-cb87-4620-94f4-ed3ce3821f7a");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e62ab376-0761-4fb6-9a45-270084291888", "8f3fe8df-fa03-4fae-9752-fa102410c5b8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e62ab376-0761-4fb6-9a45-270084291888");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8f3fe8df-fa03-4fae-9752-fa102410c5b8");

            migrationBuilder.CreateTable(
                name: "TestTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTypes", x => x.Id);
                });
        }
    }
}
