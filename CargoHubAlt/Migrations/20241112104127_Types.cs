using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class Types : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "ItemTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "ItemTypes",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ItemTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "ItemTypes",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "ItemTypes",
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
                name: "Name",
                table: "ItemTypes",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ItemTypes",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ItemTypes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "ItemTypes",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ItemTypes",
                newName: "created_at");

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
