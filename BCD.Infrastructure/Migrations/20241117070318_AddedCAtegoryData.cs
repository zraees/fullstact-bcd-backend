using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BCD.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCAtegoryData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedBy", "Description", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 7, new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "", "Gym", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7);
        }
    }
}
