using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class TransferUpdaters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "reference",
                table: "Transfers",
                newName: "Reference");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Transfers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Transfers",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "transfer_to",
                table: "Transfers",
                newName: "TransferTo");

            migrationBuilder.RenameColumn(
                name: "transfer_status",
                table: "Transfers",
                newName: "TransferStatus");

            migrationBuilder.RenameColumn(
                name: "transfer_from",
                table: "Transfers",
                newName: "TransferFrom");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Transfers",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "TransferItem",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "item_id",
                table: "TransferItem",
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
                table: "Transfers",
                newName: "reference");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Transfers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Transfers",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "TransferTo",
                table: "Transfers",
                newName: "transfer_to");

            migrationBuilder.RenameColumn(
                name: "TransferStatus",
                table: "Transfers",
                newName: "transfer_status");

            migrationBuilder.RenameColumn(
                name: "TransferFrom",
                table: "Transfers",
                newName: "transfer_from");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Transfers",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "TransferItem",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "TransferItem",
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
