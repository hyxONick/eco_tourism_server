using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eco_tourism_accommodation.Migrations
{
    /// <inheritdoc />
    public partial class AddAccommodationPicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PicUrl",
                table: "eco_tourism_accommodation_RoomInfo",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "eco_tourism_accommodation_RoomInfo",
                keyColumn: "Id",
                keyValue: 1,
                column: "PicUrl",
                value: "https://img.freepik.com/free-photo/luxury-classic-modern-bedroom-suite-hotel_105762-1787.jpg?ga=GA1.1.758032828.1726409039&semt=ais_hybrid");

            migrationBuilder.UpdateData(
                table: "eco_tourism_accommodation_RoomInfo",
                keyColumn: "Id",
                keyValue: 2,
                column: "PicUrl",
                value: "https://img.freepik.com/premium-photo/luxurious-bedroom-with-panoramic-views-mountains-river_1233664-2290.jpg?ga=GA1.1.758032828.1726409039&semt=ais_hybrid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicUrl",
                table: "eco_tourism_accommodation_RoomInfo");
        }
    }
}
