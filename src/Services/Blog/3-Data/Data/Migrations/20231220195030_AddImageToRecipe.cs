using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations;

/// <inheritdoc />
public partial class AddImageToRecipe : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "CoverImage",
            table: "recipes",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CoverImage",
            table: "recipes");
    }
}
