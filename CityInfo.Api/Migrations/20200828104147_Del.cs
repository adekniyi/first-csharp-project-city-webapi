using Microsoft.EntityFrameworkCore.Migrations;

namespace CityInfo.Api.Migrations
{
    public partial class Del : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pointsOfInterest_Cities_cityId",
                table: "pointsOfInterest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pointsOfInterest",
                table: "pointsOfInterest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.RenameTable(
                name: "pointsOfInterest",
                newName: "PointOfInterest");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "City");

            migrationBuilder.RenameIndex(
                name: "IX_pointsOfInterest_cityId",
                table: "PointOfInterest",
                newName: "IX_PointOfInterest_cityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PointOfInterest",
                table: "PointOfInterest",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_City",
                table: "City",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PointOfInterest_City_cityId",
                table: "PointOfInterest",
                column: "cityId",
                principalTable: "City",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointOfInterest_City_cityId",
                table: "PointOfInterest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PointOfInterest",
                table: "PointOfInterest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_City",
                table: "City");

            migrationBuilder.RenameTable(
                name: "PointOfInterest",
                newName: "pointsOfInterest");

            migrationBuilder.RenameTable(
                name: "City",
                newName: "Cities");

            migrationBuilder.RenameIndex(
                name: "IX_PointOfInterest_cityId",
                table: "pointsOfInterest",
                newName: "IX_pointsOfInterest_cityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pointsOfInterest",
                table: "pointsOfInterest",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_pointsOfInterest_Cities_cityId",
                table: "pointsOfInterest",
                column: "cityId",
                principalTable: "Cities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
