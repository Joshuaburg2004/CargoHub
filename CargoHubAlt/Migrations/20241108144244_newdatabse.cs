using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class newdatabse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Zip_Code = table.Column<string>(type: "TEXT", nullable: false),
                    Province = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    Contact_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Contact_Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Contact_Email = table.Column<string>(type: "TEXT", nullable: false),
                    Created_At = table.Column<string>(type: "TEXT", nullable: false),
                    Updated_At = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Item_id = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Item_reference = table.Column<string>(type: "TEXT", nullable: false),
                    Locations = table.Column<string>(type: "TEXT", nullable: false),
                    Total_on_hand = table.Column<int>(type: "INTEGER", nullable: false),
                    Total_expected = table.Column<int>(type: "INTEGER", nullable: false),
                    Total_ordered = table.Column<int>(type: "INTEGER", nullable: false),
                    Total_allocated = table.Column<int>(type: "INTEGER", nullable: false),
                    Total_available = table.Column<int>(type: "INTEGER", nullable: false),
                    Created_at = table.Column<string>(type: "TEXT", nullable: false),
                    Updated_at = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item_Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item_Lines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item_Lines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item_Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Uid = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    short_description = table.Column<string>(type: "TEXT", nullable: false),
                    upc_code = table.Column<string>(type: "TEXT", nullable: false),
                    model_number = table.Column<string>(type: "TEXT", nullable: false),
                    commodity_code = table.Column<string>(type: "TEXT", nullable: false),
                    item_line = table.Column<int>(type: "INTEGER", nullable: false),
                    item_group = table.Column<int>(type: "INTEGER", nullable: false),
                    item_type = table.Column<int>(type: "INTEGER", nullable: false),
                    unit_purchase_quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    unit_order_quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    pack_order_quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    supplier_id = table.Column<int>(type: "INTEGER", nullable: false),
                    supplier_code = table.Column<string>(type: "TEXT", nullable: false),
                    supplier_part_number = table.Column<string>(type: "TEXT", nullable: false),
                    created_at = table.Column<string>(type: "TEXT", nullable: false),
                    updated_at = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Warehouse_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Created_At = table.Column<string>(type: "TEXT", nullable: false),
                    Updated_At = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Source_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Order_Date = table.Column<string>(type: "TEXT", nullable: true),
                    Request_Date = table.Column<string>(type: "TEXT", nullable: true),
                    Reference = table.Column<string>(type: "TEXT", nullable: true),
                    Reference_Extra = table.Column<string>(type: "TEXT", nullable: true),
                    Order_Status = table.Column<string>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Shipping_Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Picking_Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Warehouse_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Ship_To = table.Column<int>(type: "INTEGER", nullable: false),
                    Bill_To = table.Column<int>(type: "INTEGER", nullable: false),
                    Shipment_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Total_Amount = table.Column<double>(type: "REAL", nullable: false),
                    Total_Discount = table.Column<double>(type: "REAL", nullable: false),
                    Total_Tax = table.Column<double>(type: "REAL", nullable: false),
                    Total_Surcharge = table.Column<double>(type: "REAL", nullable: false),
                    Created_At = table.Column<string>(type: "TEXT", nullable: false),
                    Updated_At = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    SourceId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderDate = table.Column<string>(type: "TEXT", nullable: true),
                    RequestDate = table.Column<string>(type: "TEXT", nullable: true),
                    ShipmentDate = table.Column<string>(type: "TEXT", nullable: true),
                    ShipmentType = table.Column<string>(type: "TEXT", nullable: true),
                    ShipmentStatus = table.Column<string>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    CarrierCode = table.Column<string>(type: "TEXT", nullable: true),
                    CarrierDescription = table.Column<string>(type: "TEXT", nullable: true),
                    ServiceCode = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentType = table.Column<string>(type: "TEXT", nullable: true),
                    TransferMode = table.Column<string>(type: "TEXT", nullable: true),
                    TotalPackageCount = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalPackageWeight = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Address_Extra = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Zip_Code = table.Column<string>(type: "TEXT", nullable: false),
                    Province = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    Contact_Name = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Reference = table.Column<string>(type: "TEXT", nullable: false),
                    Created_At = table.Column<string>(type: "TEXT", nullable: false),
                    Updated_At = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Reference = table.Column<string>(type: "TEXT", nullable: false),
                    Transfer_From = table.Column<int>(type: "INTEGER", nullable: false),
                    Transfer_To = table.Column<int>(type: "INTEGER", nullable: false),
                    Transfer_Status = table.Column<string>(type: "TEXT", nullable: false),
                    Created_At = table.Column<string>(type: "TEXT", nullable: false),
                    Updated_At = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Zip = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Province = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    Contact_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Contact_Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Contact_Email = table.Column<string>(type: "TEXT", nullable: false),
                    Contact_CreatedAt = table.Column<string>(type: "TEXT", nullable: false),
                    Contact_UpdatedAt = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderedItem",
                columns: table => new
                {
                    Item_Id = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedItem", x => x.Item_Id);
                    table.ForeignKey(
                        name: "FK_OrderedItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentItem",
                columns: table => new
                {
                    ItemId = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    ShipmentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentItem", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_ShipmentItem_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferItem",
                columns: table => new
                {
                    Item_Id = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    TransferId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferItem", x => x.Item_Id);
                    table.ForeignKey(
                        name: "FK_TransferItem_Transfers_TransferId",
                        column: x => x.TransferId,
                        principalTable: "Transfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderedItem_OrderId",
                table: "OrderedItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentItem_ShipmentId",
                table: "ShipmentItem",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItem_TransferId",
                table: "TransferItem",
                column: "TransferId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Item_Groups");

            migrationBuilder.DropTable(
                name: "Item_Lines");

            migrationBuilder.DropTable(
                name: "Item_Types");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "OrderedItem");

            migrationBuilder.DropTable(
                name: "ShipmentItem");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "TransferItem");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "Transfers");
        }
    }
}
