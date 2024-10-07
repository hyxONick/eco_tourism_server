using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eco_tourism_tourist.Migrations
{
    /// <inheritdoc />
    public partial class AddSceneryInfoSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "eco_tourism_tourist_SceneryInfo",
                columns: new[] { "Id", "Address", "CreatedAt", "Description", "IsDeleted", "Latitude", "Longitude", "Name", "PicUrl", "Type", "UpdatedAt" },
                values: new object[] { 1, "address", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "River Boat Cruise", "https://images.unsplash.com/photo-1707007694363-b8afb46ed639?q=80&w=2071&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "eco_tourism_tourist_SceneryInfo",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
