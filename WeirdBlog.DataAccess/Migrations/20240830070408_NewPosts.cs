using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeirdBlog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NewPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryEighteen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEighteen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostsEighteen",
                columns: table => new
                {
                    PostEighteenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryEighteenId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsEighteen", x => x.PostEighteenId);
                    table.ForeignKey(
                        name: "FK_PostsEighteen_CategoryEighteen_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryEighteen",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostsEighteen_CategoryId",
                table: "PostsEighteen",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostsEighteen");

            migrationBuilder.DropTable(
                name: "CategoryEighteen");
        }
    }
}
