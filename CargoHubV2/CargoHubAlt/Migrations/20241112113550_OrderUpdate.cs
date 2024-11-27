using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class OrderUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Warehouse_Id",
                table: "Orders",
                newName: "warehouse_id");

            migrationBuilder.RenameColumn(
                name: "Total_Tax",
                table: "Orders",
                newName: "total_tax");

            migrationBuilder.RenameColumn(
                name: "Total_Surcharge",
                table: "Orders",
                newName: "total_surcharge");

            migrationBuilder.RenameColumn(
                name: "Total_Discount",
                table: "Orders",
                newName: "total_discount");

            migrationBuilder.RenameColumn(
                name: "Total_Amount",
                table: "Orders",
                newName: "total_amount");

            migrationBuilder.RenameColumn(
                name: "Source_Id",
                table: "Orders",
                newName: "source_id");

            migrationBuilder.RenameColumn(
                name: "Shipping_Notes",
                table: "Orders",
                newName: "shipping_notes");

            migrationBuilder.RenameColumn(
                name: "Shipment_Id",
                table: "Orders",
                newName: "shipment_id");

            migrationBuilder.RenameColumn(
                name: "Ship_To",
                table: "Orders",
                newName: "ship_to");

            migrationBuilder.RenameColumn(
                name: "Request_Date",
                table: "Orders",
                newName: "request_date");

            migrationBuilder.RenameColumn(
                name: "Reference_Extra",
                table: "Orders",
                newName: "reference_extra");

            migrationBuilder.RenameColumn(
                name: "Reference",
                table: "Orders",
                newName: "reference");

            migrationBuilder.RenameColumn(
                name: "Picking_Notes",
                table: "Orders",
                newName: "picking_notes");

            migrationBuilder.RenameColumn(
                name: "Order_Status",
                table: "Orders",
                newName: "order_status");

            migrationBuilder.RenameColumn(
                name: "Order_Date",
                table: "Orders",
                newName: "order_date");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Orders",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Bill_To",
                table: "Orders",
                newName: "bill_to");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Item_Id",
                table: "OrderedItem",
                newName: "item_id");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "OrderedItem",
                newName: "amount");

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
                name: "warehouse_id",
                table: "Orders",
                newName: "Warehouse_Id");

            migrationBuilder.RenameColumn(
                name: "total_tax",
                table: "Orders",
                newName: "Total_Tax");

            migrationBuilder.RenameColumn(
                name: "total_surcharge",
                table: "Orders",
                newName: "Total_Surcharge");

            migrationBuilder.RenameColumn(
                name: "total_discount",
                table: "Orders",
                newName: "Total_Discount");

            migrationBuilder.RenameColumn(
                name: "total_amount",
                table: "Orders",
                newName: "Total_Amount");

            migrationBuilder.RenameColumn(
                name: "source_id",
                table: "Orders",
                newName: "Source_Id");

            migrationBuilder.RenameColumn(
                name: "shipping_notes",
                table: "Orders",
                newName: "Shipping_Notes");

            migrationBuilder.RenameColumn(
                name: "shipment_id",
                table: "Orders",
                newName: "Shipment_Id");

            migrationBuilder.RenameColumn(
                name: "ship_to",
                table: "Orders",
                newName: "Ship_To");

            migrationBuilder.RenameColumn(
                name: "request_date",
                table: "Orders",
                newName: "Request_Date");

            migrationBuilder.RenameColumn(
                name: "reference_extra",
                table: "Orders",
                newName: "Reference_Extra");

            migrationBuilder.RenameColumn(
                name: "reference",
                table: "Orders",
                newName: "Reference");

            migrationBuilder.RenameColumn(
                name: "picking_notes",
                table: "Orders",
                newName: "Picking_Notes");

            migrationBuilder.RenameColumn(
                name: "order_status",
                table: "Orders",
                newName: "Order_Status");

            migrationBuilder.RenameColumn(
                name: "order_date",
                table: "Orders",
                newName: "Order_Date");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "Orders",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "bill_to",
                table: "Orders",
                newName: "Bill_To");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Orders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "item_id",
                table: "OrderedItem",
                newName: "Item_Id");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "OrderedItem",
                newName: "Amount");

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
