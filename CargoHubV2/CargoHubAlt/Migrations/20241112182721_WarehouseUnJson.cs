using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class WarehouseUnJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "zip",
                table: "Warehouses",
                newName: "Zip");

            migrationBuilder.RenameColumn(
                name: "province",
                table: "Warehouses",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Warehouses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "Warehouses",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "contact_phone",
                table: "Warehouses",
                newName: "Contact_Phone");

            migrationBuilder.RenameColumn(
                name: "contact_name",
                table: "Warehouses",
                newName: "Contact_Name");

            migrationBuilder.RenameColumn(
                name: "contact_email",
                table: "Warehouses",
                newName: "Contact_Email");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "Warehouses",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Warehouses",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Warehouses",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Warehouses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Warehouses",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Warehouses",
                newName: "CreatedAt");

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
                name: "Zip",
                table: "Warehouses",
                newName: "zip");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "Warehouses",
                newName: "province");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Warehouses",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Warehouses",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "Contact_Phone",
                table: "Warehouses",
                newName: "contact_phone");

            migrationBuilder.RenameColumn(
                name: "Contact_Name",
                table: "Warehouses",
                newName: "contact_name");

            migrationBuilder.RenameColumn(
                name: "Contact_Email",
                table: "Warehouses",
                newName: "contact_email");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Warehouses",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Warehouses",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Warehouses",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Warehouses",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Warehouses",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Warehouses",
                newName: "created_at");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Warehouses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);
        }
    }
}
