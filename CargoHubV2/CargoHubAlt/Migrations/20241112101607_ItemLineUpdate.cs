using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoHubAlt.Migrations
{
    /// <inheritdoc />
    public partial class ItemLineUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Updated_At",
                table: "ItemLines",
                newName: "updated_At");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ItemLines",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ItemLines",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Created_At",
                table: "ItemLines",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ItemLines",
                newName: "id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Warehouses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "ItemLines",
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
                name: "updated_At",
                table: "ItemLines",
                newName: "Updated_At");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ItemLines",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "ItemLines",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "ItemLines",
                newName: "Created_At");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ItemLines",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Warehouses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ItemLines",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);
        }
    }
}
