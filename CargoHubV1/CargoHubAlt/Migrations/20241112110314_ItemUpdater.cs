using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class ItemUpdater : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "Items",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "upc_code",
                table: "Items",
                newName: "UpcCode");

            migrationBuilder.RenameColumn(
                name: "unit_purchase_quantity",
                table: "Items",
                newName: "UnitPurchaseQuantity");

            migrationBuilder.RenameColumn(
                name: "unit_order_quantity",
                table: "Items",
                newName: "UnitOrderQuantity");

            migrationBuilder.RenameColumn(
                name: "supplier_part_number",
                table: "Items",
                newName: "SupplierPartNumber");

            migrationBuilder.RenameColumn(
                name: "supplier_id",
                table: "Items",
                newName: "SupplierId");

            migrationBuilder.RenameColumn(
                name: "supplier_code",
                table: "Items",
                newName: "SupplierCode");

            migrationBuilder.RenameColumn(
                name: "short_description",
                table: "Items",
                newName: "ShortDescription");

            migrationBuilder.RenameColumn(
                name: "pack_order_quantity",
                table: "Items",
                newName: "PackOrderQuantity");

            migrationBuilder.RenameColumn(
                name: "model_number",
                table: "Items",
                newName: "ModelNumber");

            migrationBuilder.RenameColumn(
                name: "item_type",
                table: "Items",
                newName: "ItemType");

            migrationBuilder.RenameColumn(
                name: "item_line",
                table: "Items",
                newName: "ItemLine");

            migrationBuilder.RenameColumn(
                name: "item_group",
                table: "Items",
                newName: "ItemGroup");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Items",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "commodity_code",
                table: "Items",
                newName: "CommodityCode");

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
                name: "UpdatedAt",
                table: "Items",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "UpcCode",
                table: "Items",
                newName: "upc_code");

            migrationBuilder.RenameColumn(
                name: "UnitPurchaseQuantity",
                table: "Items",
                newName: "unit_purchase_quantity");

            migrationBuilder.RenameColumn(
                name: "UnitOrderQuantity",
                table: "Items",
                newName: "unit_order_quantity");

            migrationBuilder.RenameColumn(
                name: "SupplierPartNumber",
                table: "Items",
                newName: "supplier_part_number");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "Items",
                newName: "supplier_id");

            migrationBuilder.RenameColumn(
                name: "SupplierCode",
                table: "Items",
                newName: "supplier_code");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "Items",
                newName: "short_description");

            migrationBuilder.RenameColumn(
                name: "PackOrderQuantity",
                table: "Items",
                newName: "pack_order_quantity");

            migrationBuilder.RenameColumn(
                name: "ModelNumber",
                table: "Items",
                newName: "model_number");

            migrationBuilder.RenameColumn(
                name: "ItemType",
                table: "Items",
                newName: "item_type");

            migrationBuilder.RenameColumn(
                name: "ItemLine",
                table: "Items",
                newName: "item_line");

            migrationBuilder.RenameColumn(
                name: "ItemGroup",
                table: "Items",
                newName: "item_group");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Items",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CommodityCode",
                table: "Items",
                newName: "commodity_code");

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
