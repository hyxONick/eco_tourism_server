using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eco_tourism_accommodation.Migrations
{
    /// <inheritdoc />
    public partial class AddAccommodationDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "eco_tourism_accommodation_RoomInfo",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "See the highlights of London via 2 classic modes of transport on this half-day adventure. First, you will enjoy great views of Westminster Abbey, the Houses of Parliament, and the London Eye, as you meander through the historic streets on board a vintage double decker bus. Continue to see St. Paul’s Cathedral, Sir Christopher Wren’s architectural masterpiece, where Admirals Nelson and Wellington are buried, and Princess Diana and Prince Charles got married. Continue to the Tower of London, built nearly 1,000 years ago during the reign of William the Conqueror. Home to the Crown Jewels, the Tower is protected by the famous Beefeaters, and the imposing palace has been used as a fortress and a prison throughout its history. Your guide will take you to Traitors Gate, where prisoners entered the Tower for the last time. Next, take a short trip along the River Thames, passing Shakespeare’s Globe, Cleopatra’s Needle, and London Bridge, before arriving at Westminster Pier. Rejoin the bus and head for Buckingham Palace. Make your way to the perfect spot to watch the world famous Changing of the Guard ceremony as the soldiers, dressed in their fabulous tunics and busbies, march to military music.");

            migrationBuilder.UpdateData(
                table: "eco_tourism_accommodation_RoomInfo",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "eco_tourism_accommodation_RoomInfo",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "eco_tourism_accommodation_RoomInfo",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "");
        }
    }
}
