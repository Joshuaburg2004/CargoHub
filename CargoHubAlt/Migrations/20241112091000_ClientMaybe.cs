using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class ClientMaybe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Zip_Code",
                table: "Clients",
                newName: "zip_code");

            migrationBuilder.RenameColumn(
                name: "Updated_At",
                table: "Clients",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "Clients",
                newName: "province");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Clients",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Created_At",
                table: "Clients",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Clients",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "Contact_Phone",
                table: "Clients",
                newName: "contact_phone");

            migrationBuilder.RenameColumn(
                name: "Contact_Name",
                table: "Clients",
                newName: "contact_name");

            migrationBuilder.RenameColumn(
                name: "Contact_Email",
                table: "Clients",
                newName: "contact_email");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Clients",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Clients",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Clients",
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
                table: "Clients",
                newName: "Zip_Code");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Clients",
                newName: "Updated_At");

            migrationBuilder.RenameColumn(
                name: "province",
                table: "Clients",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Clients",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Clients",
                newName: "Created_At");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "Clients",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "contact_phone",
                table: "Clients",
                newName: "Contact_Phone");

            migrationBuilder.RenameColumn(
                name: "contact_name",
                table: "Clients",
                newName: "Contact_Name");

            migrationBuilder.RenameColumn(
                name: "contact_email",
                table: "Clients",
                newName: "Contact_Email");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Clients",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Clients",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Clients",
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
