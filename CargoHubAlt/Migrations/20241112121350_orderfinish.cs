using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class Orderfinish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "reference",
                table: "Orders",
                newName: "Reference");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "Orders",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Orders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "warehouse_id",
                table: "Orders",
                newName: "WarehouseId");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Orders",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "total_tax",
                table: "Orders",
                newName: "TotalTax");

            migrationBuilder.RenameColumn(
                name: "total_surcharge",
                table: "Orders",
                newName: "TotalSurcharge");

            migrationBuilder.RenameColumn(
                name: "total_discount",
                table: "Orders",
                newName: "TotalDiscount");

            migrationBuilder.RenameColumn(
                name: "total_amount",
                table: "Orders",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "source_id",
                table: "Orders",
                newName: "SourceId");

            migrationBuilder.RenameColumn(
                name: "shipping_notes",
                table: "Orders",
                newName: "ShippingNotes");

            migrationBuilder.RenameColumn(
                name: "shipment_id",
                table: "Orders",
                newName: "ShipmentId");

            migrationBuilder.RenameColumn(
                name: "ship_to",
                table: "Orders",
                newName: "ShipTo");

            migrationBuilder.RenameColumn(
                name: "request_date",
                table: "Orders",
                newName: "RequestDate");

            migrationBuilder.RenameColumn(
                name: "reference_extra",
                table: "Orders",
                newName: "ReferenceExtra");

            migrationBuilder.RenameColumn(
                name: "picking_notes",
                table: "Orders",
                newName: "PickingNotes");

            migrationBuilder.RenameColumn(
                name: "order_status",
                table: "Orders",
                newName: "OrderStatus");

            migrationBuilder.RenameColumn(
                name: "order_date",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Orders",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "bill_to",
                table: "Orders",
                newName: "BillTo");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "OrderedItem",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "item_id",
                table: "OrderedItem",
                newName: "ItemId");

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
                name: "Reference",
                table: "Orders",
                newName: "reference");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Orders",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "Orders",
                newName: "warehouse_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Orders",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "TotalTax",
                table: "Orders",
                newName: "total_tax");

            migrationBuilder.RenameColumn(
                name: "TotalSurcharge",
                table: "Orders",
                newName: "total_surcharge");

            migrationBuilder.RenameColumn(
                name: "TotalDiscount",
                table: "Orders",
                newName: "total_discount");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Orders",
                newName: "total_amount");

            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "Orders",
                newName: "source_id");

            migrationBuilder.RenameColumn(
                name: "ShippingNotes",
                table: "Orders",
                newName: "shipping_notes");

            migrationBuilder.RenameColumn(
                name: "ShipmentId",
                table: "Orders",
                newName: "shipment_id");

            migrationBuilder.RenameColumn(
                name: "ShipTo",
                table: "Orders",
                newName: "ship_to");

            migrationBuilder.RenameColumn(
                name: "RequestDate",
                table: "Orders",
                newName: "request_date");

            migrationBuilder.RenameColumn(
                name: "ReferenceExtra",
                table: "Orders",
                newName: "reference_extra");

            migrationBuilder.RenameColumn(
                name: "PickingNotes",
                table: "Orders",
                newName: "picking_notes");

            migrationBuilder.RenameColumn(
                name: "OrderStatus",
                table: "Orders",
                newName: "order_status");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Orders",
                newName: "order_date");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Orders",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BillTo",
                table: "Orders",
                newName: "bill_to");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "OrderedItem",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "OrderedItem",
                newName: "item_id");

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
