using Microsoft.EntityFrameworkCore.Migrations;

namespace CityInfo.Api.Migrations
{
    public partial class SampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "id", "Description", "Name" },
                values: new object[] { 1, "The one with that big park.", "New York City" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "id", "Description", "Name" },
                values: new object[] { 2, "The one with the cathedral that was never really finished.", "Antwerp" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "id", "Description", "Name" },
                values: new object[] { 3, "The one with that big tower.", "Paris" });

            migrationBuilder.InsertData(
                table: "pointsOfInterest",
                columns: new[] { "id", "Description", "Name", "cityId" },
                values: new object[,]
                {
                    { 1, "The most visited urban park in the United States.", "Central Park", 1 },
                    { 2, "A 102-story skyscraper located in Midtown Manhattan.", "Empire State Building", 1 },
                    { 3, "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans.", "Cathedral", 2 },
                    { 4, "The the finest example of railway architecture in Belgium.", "Antwerp Central Station", 2 },
                    { 5, "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel.", "Eiffel Tower", 3 },
                    { 6, "The world's largest museum.", "The Louvre", 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "pointsOfInterest",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "pointsOfInterest",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "pointsOfInterest",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "pointsOfInterest",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "pointsOfInterest",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "pointsOfInterest",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
