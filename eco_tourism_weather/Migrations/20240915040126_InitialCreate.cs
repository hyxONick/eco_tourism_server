using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eco_tourism_weather.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "eco_tourism_tourist_WeatherInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Region = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Country = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitude = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TzId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LocaltimeEpoch = table.Column<long>(type: "bigint", nullable: false),
                    Localtime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdatedEpoch = table.Column<long>(type: "bigint", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TempC = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TempF = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    IsDay = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ConditionText = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConditionIcon = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConditionCode = table.Column<int>(type: "int", nullable: false),
                    WindMph = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    WindKph = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    WindDegree = table.Column<int>(type: "int", nullable: false),
                    PrecipMm = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    Cloud = table.Column<int>(type: "int", nullable: false),
                    FeelslikeC = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    FeelslikeF = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    WindchillC = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    WindchillF = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    HeatindexC = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    VisKm = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    UV = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    GustMph = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    GustKph = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eco_tourism_tourist_WeatherInfo", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eco_tourism_tourist_WeatherInfo");
        }
    }
}
