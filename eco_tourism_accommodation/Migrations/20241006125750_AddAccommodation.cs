using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eco_tourism_accommodation.Migrations
{
    /// <inheritdoc />
    public partial class AddAccommodation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "eco_tourism_accommodation_RoomInfo",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "eco_tourism_accommodation_RoomInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBeds",
                table: "eco_tourism_accommodation_RoomInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "eco_tourism_accommodation_RoomInfo",
                columns: new[] { "Id", "Address", "Capacity", "CreatedAt", "CreditPrice", "Description", "IsDeleted", "NumberOfBeds", "Price", "RoomName", "RoomType", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Downtown City Center", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "", false, 1, 180.0, "Luxury Classic Suite", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "English Countryside", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "", false, 1, 240.0, "Cotswolds Cottage Retreat", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "eco_tourism_accommodation_RoomInfo",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "eco_tourism_accommodation_RoomInfo",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Address",
                table: "eco_tourism_accommodation_RoomInfo");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "eco_tourism_accommodation_RoomInfo");

            migrationBuilder.DropColumn(
                name: "NumberOfBeds",
                table: "eco_tourism_accommodation_RoomInfo");
        }
    }
}
