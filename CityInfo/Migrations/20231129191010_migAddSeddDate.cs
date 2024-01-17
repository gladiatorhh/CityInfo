using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CityInfo.Migrations
{
    /// <inheritdoc />
    public partial class migAddSeddDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PointsOfInterest",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "London is a very old an beautiful city", "London" },
                    { 2, "Paris is a very old an beautiful city", "Paris" },
                    { 3, "NewYork is a very old an beautiful city", "NewYork" }
                });

            migrationBuilder.InsertData(
                table: "PointsOfInterest",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 1, "This church belongs to first century", "The old church" },
                    { 2, 1, "This bridge belongs to first century", "The old bridge" },
                    { 3, 1, "This Castle belongs to first century", "The old Castle" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PointsOfInterest",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PointsOfInterest",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PointsOfInterest",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PointsOfInterest");
        }
    }
}
