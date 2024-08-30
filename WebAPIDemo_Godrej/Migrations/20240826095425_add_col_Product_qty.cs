using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIDemo_Godrej.Migrations
{
    /// <inheritdoc />
    public partial class add_col_Product_qty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Qty",
                table: "Product",
                type: "numeric(5,0)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qty",
                table: "Product");
        }
    }
}
