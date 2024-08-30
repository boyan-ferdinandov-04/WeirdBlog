using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeirdBlog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CategoriesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostsEighteen_CategoryEighteen_CategoryId",
                table: "PostsEighteen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryEighteen",
                table: "CategoryEighteen");

            migrationBuilder.RenameTable(
                name: "CategoryEighteen",
                newName: "CategoriesEighteen");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriesEighteen",
                table: "CategoriesEighteen",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostsEighteen_CategoriesEighteen_CategoryId",
                table: "PostsEighteen",
                column: "CategoryId",
                principalTable: "CategoriesEighteen",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostsEighteen_CategoriesEighteen_CategoryId",
                table: "PostsEighteen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriesEighteen",
                table: "CategoriesEighteen");

            migrationBuilder.RenameTable(
                name: "CategoriesEighteen",
                newName: "CategoryEighteen");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryEighteen",
                table: "CategoryEighteen",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostsEighteen_CategoryEighteen_CategoryId",
                table: "PostsEighteen",
                column: "CategoryId",
                principalTable: "CategoryEighteen",
                principalColumn: "Id");
        }
    }
}
