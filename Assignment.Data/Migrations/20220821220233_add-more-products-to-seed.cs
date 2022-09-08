using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment.Data.Migrations
{
    public partial class addmoreproductstoseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ImageUrl", "Name", "Price", "ProductCategoryId", "Quantity" },
                values: new object[,]
                {
                    { 11L, null, "Product 11", 0m, 1L, 0 },
                    { 34L, null, "Product 34", 0m, 3L, 0 },
                    { 44L, null, "Product 44", 0m, 4L, 0 },
                    { 54L, null, "Product 54", 0m, 5L, 0 },
                    { 64L, null, "Product 64", 0m, 6L, 0 },
                    { 15L, null, "Product 15", 0m, 1L, 0 },
                    { 25L, null, "Product 25", 0m, 2L, 0 },
                    { 24L, null, "Product 24", 0m, 2L, 0 },
                    { 35L, null, "Product 35", 0m, 3L, 0 },
                    { 55L, null, "Product 55", 0m, 5L, 0 },
                    { 65L, null, "Product 65", 0m, 6L, 0 },
                    { 16L, null, "Product 16", 0m, 1L, 0 },
                    { 26L, null, "Product 26", 0m, 2L, 0 },
                    { 36L, null, "Product 36", 0m, 3L, 0 },
                    { 46L, null, "Product 46", 0m, 4L, 0 },
                    { 45L, null, "Product 45", 0m, 4L, 0 },
                    { 14L, null, "Product 14", 0m, 1L, 0 },
                    { 63L, null, "Product 63", 0m, 6L, 0 },
                    { 53L, null, "Product 53", 0m, 5L, 0 },
                    { 21L, null, "Product 21", 0m, 2L, 0 },
                    { 31L, null, "Product 31", 0m, 3L, 0 },
                    { 41L, null, "Product 41", 0m, 4L, 0 },
                    { 51L, null, "Product 51", 0m, 5L, 0 },
                    { 61L, null, "Product 61", 0m, 6L, 0 },
                    { 12L, null, "Product 12", 0m, 1L, 0 },
                    { 22L, null, "Product 22", 0m, 2L, 0 },
                    { 32L, null, "Product 32", 0m, 3L, 0 },
                    { 42L, null, "Product 42", 0m, 4L, 0 },
                    { 52L, null, "Product 52", 0m, 5L, 0 },
                    { 62L, null, "Product 62", 0m, 6L, 0 },
                    { 13L, null, "Product 13", 0m, 1L, 0 },
                    { 23L, null, "Product 23", 0m, 2L, 0 },
                    { 33L, null, "Product 33", 0m, 3L, 0 },
                    { 43L, null, "Product 43", 0m, 4L, 0 },
                    { 56L, null, "Product 56", 0m, 5L, 0 },
                    { 66L, null, "Product 66", 0m, 6L, 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 66L);
        }
    }
}
