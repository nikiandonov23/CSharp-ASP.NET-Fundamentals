using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoppingListApp2025.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNotesToTheProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductNotes",
                columns: new[] { "Id", "Content", "ProductId" },
                values: new object[,]
                {
                    { 1, "Buy Gouda", 1 },
                    { 2, "2 Liters", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductNotes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductNotes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
