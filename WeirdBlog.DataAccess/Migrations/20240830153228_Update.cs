using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeirdBlog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostsEighteen_CategoriesEighteen_CategoryId",
                table: "PostsEighteen");

            migrationBuilder.DropIndex(
                name: "IX_PostsEighteen_CategoryId",
                table: "PostsEighteen");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PostsEighteen");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CategoriesEighteen",
                newName: "CategoryEighteenId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsEighteen_CategoryEighteenId",
                table: "PostsEighteen",
                column: "CategoryEighteenId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostsEighteen_CategoriesEighteen_CategoryEighteenId",
                table: "PostsEighteen",
                column: "CategoryEighteenId",
                principalTable: "CategoriesEighteen",
                principalColumn: "CategoryEighteenId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostsEighteen_CategoriesEighteen_CategoryEighteenId",
                table: "PostsEighteen");

            migrationBuilder.DropIndex(
                name: "IX_PostsEighteen_CategoryEighteenId",
                table: "PostsEighteen");

            migrationBuilder.RenameColumn(
                name: "CategoryEighteenId",
                table: "CategoriesEighteen",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "PostsEighteen",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostsEighteen_CategoryId",
                table: "PostsEighteen",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostsEighteen_CategoriesEighteen_CategoryId",
                table: "PostsEighteen",
                column: "CategoryId",
                principalTable: "CategoriesEighteen",
                principalColumn: "Id");
        }
    }
}
