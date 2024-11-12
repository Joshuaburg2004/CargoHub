using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class InventoryUpdater : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "locations",
                table: "Inventories",
                newName: "Locations");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Inventories",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Inventories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Inventories",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "total_ordered",
                table: "Inventories",
                newName: "TotalOrdered");

            migrationBuilder.RenameColumn(
                name: "total_on_hand",
                table: "Inventories",
                newName: "TotalOnHand");

            migrationBuilder.RenameColumn(
                name: "total_expected",
                table: "Inventories",
                newName: "TotalExpected");

            migrationBuilder.RenameColumn(
                name: "total_available",
                table: "Inventories",
                newName: "TotalAvailable");

            migrationBuilder.RenameColumn(
                name: "total_allocated",
                table: "Inventories",
                newName: "TotalAllocated");

            migrationBuilder.RenameColumn(
                name: "item_reference",
                table: "Inventories",
                newName: "ItemReference");

            migrationBuilder.RenameColumn(
                name: "item_id",
                table: "Inventories",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Inventories",
                newName: "CreatedAt");

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
                name: "Locations",
                table: "Inventories",
                newName: "locations");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Inventories",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Inventories",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Inventories",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "TotalOrdered",
                table: "Inventories",
                newName: "total_ordered");

            migrationBuilder.RenameColumn(
                name: "TotalOnHand",
                table: "Inventories",
                newName: "total_on_hand");

            migrationBuilder.RenameColumn(
                name: "TotalExpected",
                table: "Inventories",
                newName: "total_expected");

            migrationBuilder.RenameColumn(
                name: "TotalAvailable",
                table: "Inventories",
                newName: "total_available");

            migrationBuilder.RenameColumn(
                name: "TotalAllocated",
                table: "Inventories",
                newName: "total_allocated");

            migrationBuilder.RenameColumn(
                name: "ItemReference",
                table: "Inventories",
                newName: "item_reference");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Inventories",
                newName: "item_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Inventories",
                newName: "created_at");

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
