using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class InventoryUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Items",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Items",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "Items",
                newName: "uid");

            migrationBuilder.RenameColumn(
                name: "Updated_at",
                table: "Inventories",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "Total_ordered",
                table: "Inventories",
                newName: "total_ordered");

            migrationBuilder.RenameColumn(
                name: "Total_on_hand",
                table: "Inventories",
                newName: "total_on_hand");

            migrationBuilder.RenameColumn(
                name: "Total_expected",
                table: "Inventories",
                newName: "total_expected");

            migrationBuilder.RenameColumn(
                name: "Total_available",
                table: "Inventories",
                newName: "total_available");

            migrationBuilder.RenameColumn(
                name: "Total_allocated",
                table: "Inventories",
                newName: "total_allocated");

            migrationBuilder.RenameColumn(
                name: "Locations",
                table: "Inventories",
                newName: "locations");

            migrationBuilder.RenameColumn(
                name: "Item_reference",
                table: "Inventories",
                newName: "item_reference");

            migrationBuilder.RenameColumn(
                name: "Item_id",
                table: "Inventories",
                newName: "item_id");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Inventories",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Created_at",
                table: "Inventories",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Inventories",
                newName: "id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Warehouses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "Items",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "Items",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "uid",
                table: "Items",
                newName: "Uid");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Inventories",
                newName: "Updated_at");

            migrationBuilder.RenameColumn(
                name: "total_ordered",
                table: "Inventories",
                newName: "Total_ordered");

            migrationBuilder.RenameColumn(
                name: "total_on_hand",
                table: "Inventories",
                newName: "Total_on_hand");

            migrationBuilder.RenameColumn(
                name: "total_expected",
                table: "Inventories",
                newName: "Total_expected");

            migrationBuilder.RenameColumn(
                name: "total_available",
                table: "Inventories",
                newName: "Total_available");

            migrationBuilder.RenameColumn(
                name: "total_allocated",
                table: "Inventories",
                newName: "Total_allocated");

            migrationBuilder.RenameColumn(
                name: "locations",
                table: "Inventories",
                newName: "Locations");

            migrationBuilder.RenameColumn(
                name: "item_reference",
                table: "Inventories",
                newName: "Item_reference");

            migrationBuilder.RenameColumn(
                name: "item_id",
                table: "Inventories",
                newName: "Item_id");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Inventories",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Inventories",
                newName: "Created_at");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Inventories",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Warehouses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);
        }
    }
}
