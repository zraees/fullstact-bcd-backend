using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BCD.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserTypeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "UserTypeId", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy", "UserTypeName" },
                values: new object[] { 2, new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, "User" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 2);
        }
    }
}
