using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class ShipmentsUpdaters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "notes",
                table: "Shipments",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Shipments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Shipments",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "transfer_mode",
                table: "Shipments",
                newName: "TransferMode");

            migrationBuilder.RenameColumn(
                name: "total_package_weight",
                table: "Shipments",
                newName: "TotalPackageWeight");

            migrationBuilder.RenameColumn(
                name: "total_package_count",
                table: "Shipments",
                newName: "TotalPackageCount");

            migrationBuilder.RenameColumn(
                name: "source_id",
                table: "Shipments",
                newName: "SourceId");

            migrationBuilder.RenameColumn(
                name: "shipment_type",
                table: "Shipments",
                newName: "ShipmentType");

            migrationBuilder.RenameColumn(
                name: "shipment_status",
                table: "Shipments",
                newName: "ShipmentStatus");

            migrationBuilder.RenameColumn(
                name: "shipment_date",
                table: "Shipments",
                newName: "ShipmentDate");

            migrationBuilder.RenameColumn(
                name: "service_code",
                table: "Shipments",
                newName: "ServiceCode");

            migrationBuilder.RenameColumn(
                name: "request_date",
                table: "Shipments",
                newName: "RequestDate");

            migrationBuilder.RenameColumn(
                name: "payment_type",
                table: "Shipments",
                newName: "PaymentType");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "Shipments",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "order_date",
                table: "Shipments",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Shipments",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "carrier_description",
                table: "Shipments",
                newName: "CarrierDescription");

            migrationBuilder.RenameColumn(
                name: "carrier_code",
                table: "Shipments",
                newName: "CarrierCode");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "ShipmentItem",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "item_id",
                table: "ShipmentItem",
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
                name: "Notes",
                table: "Shipments",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Shipments",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Shipments",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "TransferMode",
                table: "Shipments",
                newName: "transfer_mode");

            migrationBuilder.RenameColumn(
                name: "TotalPackageWeight",
                table: "Shipments",
                newName: "total_package_weight");

            migrationBuilder.RenameColumn(
                name: "TotalPackageCount",
                table: "Shipments",
                newName: "total_package_count");

            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "Shipments",
                newName: "source_id");

            migrationBuilder.RenameColumn(
                name: "ShipmentType",
                table: "Shipments",
                newName: "shipment_type");

            migrationBuilder.RenameColumn(
                name: "ShipmentStatus",
                table: "Shipments",
                newName: "shipment_status");

            migrationBuilder.RenameColumn(
                name: "ShipmentDate",
                table: "Shipments",
                newName: "shipment_date");

            migrationBuilder.RenameColumn(
                name: "ServiceCode",
                table: "Shipments",
                newName: "service_code");

            migrationBuilder.RenameColumn(
                name: "RequestDate",
                table: "Shipments",
                newName: "request_date");

            migrationBuilder.RenameColumn(
                name: "PaymentType",
                table: "Shipments",
                newName: "payment_type");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Shipments",
                newName: "order_id");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Shipments",
                newName: "order_date");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Shipments",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CarrierDescription",
                table: "Shipments",
                newName: "carrier_description");

            migrationBuilder.RenameColumn(
                name: "CarrierCode",
                table: "Shipments",
                newName: "carrier_code");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "ShipmentItem",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "ShipmentItem",
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
