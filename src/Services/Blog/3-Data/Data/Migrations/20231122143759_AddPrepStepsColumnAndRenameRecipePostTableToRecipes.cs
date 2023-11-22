using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPrepStepsColumnAndRenameRecipePostTableToRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recipe_posts_AspNetUsers_user_id",
                table: "recipe_posts");

            migrationBuilder.DropForeignKey(
                name: "FK_recipe_posts_blogs_blog_id",
                table: "recipe_posts");

            migrationBuilder.DropForeignKey(
                name: "FK_recipe_posts_categories_category_id",
                table: "recipe_posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recipe_posts",
                table: "recipe_posts");

            migrationBuilder.DropColumn(
                name: "content",
                table: "recipe_posts");

            migrationBuilder.RenameTable(
                name: "recipe_posts",
                newName: "recipes");

            migrationBuilder.RenameColumn(
                name: "Ingredients",
                table: "recipes",
                newName: "ingredients");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_posts_user_id",
                table: "recipes",
                newName: "IX_recipes_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_posts_url",
                table: "recipes",
                newName: "IX_recipes_url");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_posts_title",
                table: "recipes",
                newName: "IX_recipes_title");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_posts_category_id",
                table: "recipes",
                newName: "IX_recipes_category_id");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_posts_blog_id",
                table: "recipes",
                newName: "IX_recipes_blog_id");

            migrationBuilder.AddColumn<string>(
                name: "preparation_steps",
                table: "recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipes",
                table: "recipes",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_recipes_AspNetUsers_user_id",
                table: "recipes",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_recipes_blogs_blog_id",
                table: "recipes",
                column: "blog_id",
                principalTable: "blogs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_recipes_categories_category_id",
                table: "recipes",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recipes_AspNetUsers_user_id",
                table: "recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_recipes_blogs_blog_id",
                table: "recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_recipes_categories_category_id",
                table: "recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recipes",
                table: "recipes");

            migrationBuilder.DropColumn(
                name: "preparation_steps",
                table: "recipes");

            migrationBuilder.RenameTable(
                name: "recipes",
                newName: "recipe_posts");

            migrationBuilder.RenameColumn(
                name: "ingredients",
                table: "recipe_posts",
                newName: "Ingredients");

            migrationBuilder.RenameIndex(
                name: "IX_recipes_user_id",
                table: "recipe_posts",
                newName: "IX_recipe_posts_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_recipes_url",
                table: "recipe_posts",
                newName: "IX_recipe_posts_url");

            migrationBuilder.RenameIndex(
                name: "IX_recipes_title",
                table: "recipe_posts",
                newName: "IX_recipe_posts_title");

            migrationBuilder.RenameIndex(
                name: "IX_recipes_category_id",
                table: "recipe_posts",
                newName: "IX_recipe_posts_category_id");

            migrationBuilder.RenameIndex(
                name: "IX_recipes_blog_id",
                table: "recipe_posts",
                newName: "IX_recipe_posts_blog_id");

            migrationBuilder.AddColumn<string>(
                name: "content",
                table: "recipe_posts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipe_posts",
                table: "recipe_posts",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_recipe_posts_AspNetUsers_user_id",
                table: "recipe_posts",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_recipe_posts_blogs_blog_id",
                table: "recipe_posts",
                column: "blog_id",
                principalTable: "blogs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_recipe_posts_categories_category_id",
                table: "recipe_posts",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
