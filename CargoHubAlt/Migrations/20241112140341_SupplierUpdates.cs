using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class SupplierUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "reference",
                table: "Suppliers",
                newName: "Reference");

            migrationBuilder.RenameColumn(
                name: "province",
                table: "Suppliers",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "phonenumber",
                table: "Suppliers",
                newName: "Phonenumber");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Suppliers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "Suppliers",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "Suppliers",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Suppliers",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Suppliers",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Suppliers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "zip_code",
                table: "Suppliers",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Suppliers",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Suppliers",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "contact_name",
                table: "Suppliers",
                newName: "ContactName");

            migrationBuilder.RenameColumn(
                name: "address_extra",
                table: "Suppliers",
                newName: "AddressExtra");

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
                table: "Suppliers",
                newName: "reference");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "Suppliers",
                newName: "province");

            migrationBuilder.RenameColumn(
                name: "Phonenumber",
                table: "Suppliers",
                newName: "phonenumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Suppliers",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Suppliers",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Suppliers",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Suppliers",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Suppliers",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Suppliers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Suppliers",
                newName: "zip_code");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Suppliers",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Suppliers",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "ContactName",
                table: "Suppliers",
                newName: "contact_name");

            migrationBuilder.RenameColumn(
                name: "AddressExtra",
                table: "Suppliers",
                newName: "address_extra");

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
