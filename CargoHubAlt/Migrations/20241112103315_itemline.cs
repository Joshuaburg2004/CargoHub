using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class Itemline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Updated_At",
                table: "ItemTypes",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ItemTypes",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ItemTypes",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Created_At",
                table: "ItemTypes",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ItemTypes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ItemLines",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ItemLines",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ItemLines",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "ItemLines",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ItemLines",
                newName: "created_at");

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
                name: "updated_at",
                table: "ItemTypes",
                newName: "Updated_At");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ItemTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "ItemTypes",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "ItemTypes",
                newName: "Created_At");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ItemTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ItemLines",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "ItemLines",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ItemLines",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "ItemLines",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "ItemLines",
                newName: "CreatedAt");

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
