using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class City3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_City_CityId",
                table: "Customer");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Customer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_City_CityId",
                table: "Customer",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_City_CityId",
                table: "Customer");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_City_CityId",
                table: "Customer",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
