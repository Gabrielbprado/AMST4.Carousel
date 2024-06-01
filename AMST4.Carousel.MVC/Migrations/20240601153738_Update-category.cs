using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMST4.Carousel.MVC.Migrations
{
    /// <inheritdoc />
    public partial class Updatecategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Category",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
