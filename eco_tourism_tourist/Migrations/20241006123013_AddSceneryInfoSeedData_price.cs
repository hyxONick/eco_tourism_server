using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eco_tourism_tourist.Migrations
{
    /// <inheritdoc />
    public partial class AddSceneryInfoSeedData_price : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "eco_tourism_tourist_SceneryInfo",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "eco_tourism_tourist_SceneryInfo",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 10.5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "eco_tourism_tourist_SceneryInfo");
        }
    }
}
