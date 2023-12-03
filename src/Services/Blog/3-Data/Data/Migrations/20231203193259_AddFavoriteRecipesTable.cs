using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations;

/// <inheritdoc />
public partial class AddFavoriteRecipesTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "user_favorite_recipes",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                recipe_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_user_favorite_recipes", x => x.id);
                table.ForeignKey(
                    name: "FK_user_favorite_recipes_AspNetUsers_user_id",
                    column: x => x.user_id,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_user_favorite_recipes_recipes_recipe_id",
                    column: x => x.recipe_id,
                    principalTable: "recipes",
                    principalColumn: "id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_user_favorite_recipes_recipe_id",
            table: "user_favorite_recipes",
            column: "recipe_id");

        migrationBuilder.CreateIndex(
            name: "IX_user_favorite_recipes_user_id",
            table: "user_favorite_recipes",
            column: "user_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "user_favorite_recipes");
    }
}
