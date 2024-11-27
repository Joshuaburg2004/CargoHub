using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class ItemGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item_Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item_Types",
                table: "Item_Types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item_Lines",
                table: "Item_Lines");

            migrationBuilder.RenameTable(
                name: "Item_Types",
                newName: "ItemTypes");

            migrationBuilder.RenameTable(
                name: "Item_Lines",
                newName: "ItemLines");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Warehouses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemTypes",
                table: "ItemTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemLines",
                table: "ItemLines",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ItemGroups",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    created_at = table.Column<string>(type: "TEXT", nullable: false),
                    updated_at = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemGroups", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemTypes",
                table: "ItemTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemLines",
                table: "ItemLines");

            migrationBuilder.RenameTable(
                name: "ItemTypes",
                newName: "Item_Types");

            migrationBuilder.RenameTable(
                name: "ItemLines",
                newName: "Item_Lines");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Warehouses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item_Types",
                table: "Item_Types",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item_Lines",
                table: "Item_Lines",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Item_Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created_At = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Updated_At = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item_Groups", x => x.Id);
                });
        }
    }
}
