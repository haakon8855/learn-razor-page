using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoPizza.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSodaNotRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Sodas_SodaId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "SodaId",
                table: "Orders",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Sodas_SodaId",
                table: "Orders",
                column: "SodaId",
                principalTable: "Sodas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Sodas_SodaId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "SodaId",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Sodas_SodaId",
                table: "Orders",
                column: "SodaId",
                principalTable: "Sodas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
