using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class SupplierUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Zip_Code",
                table: "Suppliers",
                newName: "zip_code");

            migrationBuilder.RenameColumn(
                name: "Updated_At",
                table: "Suppliers",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "Reference",
                table: "Suppliers",
                newName: "reference");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "Suppliers",
                newName: "province");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Suppliers",
                newName: "phonenumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Suppliers",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Created_At",
                table: "Suppliers",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Suppliers",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "Contact_Name",
                table: "Suppliers",
                newName: "contact_name");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Suppliers",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Suppliers",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Address_Extra",
                table: "Suppliers",
                newName: "address_extra");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Suppliers",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Suppliers",
                newName: "id");

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
                name: "zip_code",
                table: "Suppliers",
                newName: "Zip_Code");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Suppliers",
                newName: "Updated_At");

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
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Suppliers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Suppliers",
                newName: "Created_At");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "Suppliers",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "contact_name",
                table: "Suppliers",
                newName: "Contact_Name");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "Suppliers",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Suppliers",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "address_extra",
                table: "Suppliers",
                newName: "Address_Extra");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Suppliers",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Suppliers",
                newName: "Id");

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
