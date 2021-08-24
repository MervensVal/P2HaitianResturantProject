using Microsoft.EntityFrameworkCore.Migrations;

namespace HaitianRestaurantApp2.Data.Migrations
{
    public partial class removeLocationFromShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Locations_LocationId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_LocationId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "ShoppingCarts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_LocationId",
                table: "ShoppingCarts",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Locations_LocationId",
                table: "ShoppingCarts",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
