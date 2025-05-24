using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_11.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Prescription_Medicament",
                newName: "Details");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Prescription_Medicament",
                newName: "Description");
        }
    }
}
