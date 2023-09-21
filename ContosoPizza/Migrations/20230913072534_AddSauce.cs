using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoPizza.Migrations
{
    /// <inheritdoc />
    public partial class AddSauce : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SauceId",
                table: "Pizzas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sauces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IsVegan = table.Column<bool>(type: "INTEGER", nullable: false),
                    Calories = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sauces", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Sauces",
                columns: new[] { "Name", "IsVegan", "Calories" },
                values: new object[,] {
                    { "Tomato", true, 30.0 }
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Pizzas_SauceId",
                table: "Pizzas",
                column: "SauceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_Sauces_SauceId",
                table: "Pizzas",
                column: "SauceId",
                principalTable: "Sauces",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_Sauces_SauceId",
                table: "Pizzas");

            migrationBuilder.DropTable(
                name: "Sauces");

            migrationBuilder.DropIndex(
                name: "IX_Pizzas_SauceId",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "SauceId",
                table: "Pizzas");
        }
    }
}
