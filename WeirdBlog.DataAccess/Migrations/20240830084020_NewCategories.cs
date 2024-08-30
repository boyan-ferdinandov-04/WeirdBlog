using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WeirdBlog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NewCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CategoriesEighteen",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Animated" },
                    { 2, "Live-Action" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoriesEighteen",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CategoriesEighteen",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
