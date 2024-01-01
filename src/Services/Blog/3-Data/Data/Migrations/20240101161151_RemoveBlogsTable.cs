using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations;

/// <inheritdoc />
public partial class RemoveBlogsTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_categories_blogs_blog_id",
            table: "categories");

        migrationBuilder.DropForeignKey(
            name: "FK_recipes_blogs_blog_id",
            table: "recipes");

        migrationBuilder.DropTable(
            name: "user_blogs");

        migrationBuilder.DropTable(
            name: "blogs");

        migrationBuilder.DropIndex(
            name: "IX_recipes_blog_id",
            table: "recipes");

        migrationBuilder.DropIndex(
            name: "IX_categories_blog_id",
            table: "categories");

        migrationBuilder.DropColumn(
            name: "blog_id",
            table: "recipes");

        migrationBuilder.DropColumn(
            name: "blog_id",
            table: "categories");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "blog_id",
            table: "recipes",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<Guid>(
            name: "blog_id",
            table: "categories",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.CreateTable(
            name: "blogs",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                normalized_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_blogs", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "user_blogs",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                blog_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_user_blogs", x => x.id);
                table.ForeignKey(
                    name: "FK_user_blogs_AspNetUsers_user_id",
                    column: x => x.user_id,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_user_blogs_blogs_blog_id",
                    column: x => x.blog_id,
                    principalTable: "blogs",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_recipes_blog_id",
            table: "recipes",
            column: "blog_id");

        migrationBuilder.CreateIndex(
            name: "IX_categories_blog_id",
            table: "categories",
            column: "blog_id");

        migrationBuilder.CreateIndex(
            name: "IX_blogs_name",
            table: "blogs",
            column: "name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_blogs_normalized_name",
            table: "blogs",
            column: "normalized_name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_user_blogs_blog_id",
            table: "user_blogs",
            column: "blog_id");

        migrationBuilder.CreateIndex(
            name: "IX_user_blogs_user_id",
            table: "user_blogs",
            column: "user_id");

        migrationBuilder.AddForeignKey(
            name: "FK_categories_blogs_blog_id",
            table: "categories",
            column: "blog_id",
            principalTable: "blogs",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_recipes_blogs_blog_id",
            table: "recipes",
            column: "blog_id",
            principalTable: "blogs",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }
}
