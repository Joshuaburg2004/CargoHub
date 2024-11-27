using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class TransferUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Updated_At",
                table: "Transfers",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "Transfer_To",
                table: "Transfers",
                newName: "transfer_to");

            migrationBuilder.RenameColumn(
                name: "Transfer_Status",
                table: "Transfers",
                newName: "transfer_status");

            migrationBuilder.RenameColumn(
                name: "Transfer_From",
                table: "Transfers",
                newName: "transfer_from");

            migrationBuilder.RenameColumn(
                name: "Reference",
                table: "Transfers",
                newName: "reference");

            migrationBuilder.RenameColumn(
                name: "Created_At",
                table: "Transfers",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Transfers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Item_Id",
                table: "TransferItem",
                newName: "item_id");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "TransferItem",
                newName: "amount");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Warehouses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "transfer_to",
                table: "Transfers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "transfer_from",
                table: "Transfers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Transfers",
                newName: "Updated_At");

            migrationBuilder.RenameColumn(
                name: "transfer_to",
                table: "Transfers",
                newName: "Transfer_To");

            migrationBuilder.RenameColumn(
                name: "transfer_status",
                table: "Transfers",
                newName: "Transfer_Status");

            migrationBuilder.RenameColumn(
                name: "transfer_from",
                table: "Transfers",
                newName: "Transfer_From");

            migrationBuilder.RenameColumn(
                name: "reference",
                table: "Transfers",
                newName: "Reference");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Transfers",
                newName: "Created_At");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Transfers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "item_id",
                table: "TransferItem",
                newName: "Item_Id");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "TransferItem",
                newName: "Amount");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Warehouses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Transfer_To",
                table: "Transfers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Transfer_From",
                table: "Transfers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
