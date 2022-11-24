using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lamiapizzeriastatic.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigrationError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientPizza_Pizzas_PizzaId",
                table: "IngredientPizza");

            migrationBuilder.RenameColumn(
                name: "PizzaId",
                table: "IngredientPizza",
                newName: "PizzasId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientPizza_PizzaId",
                table: "IngredientPizza",
                newName: "IX_IngredientPizza_PizzasId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientPizza_Pizzas_PizzasId",
                table: "IngredientPizza",
                column: "PizzasId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientPizza_Pizzas_PizzasId",
                table: "IngredientPizza");

            migrationBuilder.RenameColumn(
                name: "PizzasId",
                table: "IngredientPizza",
                newName: "PizzaId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientPizza_PizzasId",
                table: "IngredientPizza",
                newName: "IX_IngredientPizza_PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientPizza_Pizzas_PizzaId",
                table: "IngredientPizza",
                column: "PizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
