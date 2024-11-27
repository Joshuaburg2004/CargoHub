using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class ClientModelUpdater : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Clients",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "Zip_Code",
                table: "Clients",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created_At",
                table: "Clients",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Contact_Phone",
                table: "Clients",
                newName: "ContactPhone");

            migrationBuilder.RenameColumn(
                name: "Contact_Name",
                table: "Clients",
                newName: "ContactName");

            migrationBuilder.RenameColumn(
                name: "Contact_Email",
                table: "Clients",
                newName: "ContactEmail");

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
                name: "ZipCode",
                table: "Clients",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Clients",
                newName: "Zip_Code");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Clients",
                newName: "Created_At");

            migrationBuilder.RenameColumn(
                name: "ContactPhone",
                table: "Clients",
                newName: "Contact_Phone");

            migrationBuilder.RenameColumn(
                name: "ContactName",
                table: "Clients",
                newName: "Contact_Name");

            migrationBuilder.RenameColumn(
                name: "ContactEmail",
                table: "Clients",
                newName: "Contact_Email");

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
