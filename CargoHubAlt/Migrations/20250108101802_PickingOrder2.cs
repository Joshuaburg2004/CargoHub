using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class PickingOrder2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Warehouses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "PickingOrderId",
                table: "Locations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PickingOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickingOrders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_PickingOrderId",
                table: "Locations",
                column: "PickingOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_PickingOrders_PickingOrderId",
                table: "Locations",
                column: "PickingOrderId",
                principalTable: "PickingOrders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_PickingOrders_PickingOrderId",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "PickingOrders");

            migrationBuilder.DropIndex(
                name: "IX_Locations_PickingOrderId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "PickingOrderId",
                table: "Locations");

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
