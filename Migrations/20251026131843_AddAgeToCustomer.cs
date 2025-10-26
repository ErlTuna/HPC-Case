using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappenCodeECommerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAgeToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Customers");
        }
    }
}
