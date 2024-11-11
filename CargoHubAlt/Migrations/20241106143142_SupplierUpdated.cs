using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class SupplierUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Suppliers",
                newName: "Zip_Code");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Suppliers",
                newName: "Updated_At");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Suppliers",
                newName: "Created_At");

            migrationBuilder.RenameColumn(
                name: "ContactName",
                table: "Suppliers",
                newName: "Contact_Name");

            migrationBuilder.RenameColumn(
                name: "AddressExtra",
                table: "Suppliers",
                newName: "Address_Extra");

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
                name: "Zip_Code",
                table: "Suppliers",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "Updated_At",
                table: "Suppliers",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created_At",
                table: "Suppliers",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Contact_Name",
                table: "Suppliers",
                newName: "ContactName");

            migrationBuilder.RenameColumn(
                name: "Address_Extra",
                table: "Suppliers",
                newName: "AddressExtra");

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
