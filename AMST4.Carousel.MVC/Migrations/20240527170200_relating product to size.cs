using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMST4.Carousel.MVC.Migrations
{
    /// <inheritdoc />
    public partial class Relatingproducttosize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Size_Id",
                table: "Product",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Product_Size_Id",
                table: "Product",
                column: "Size_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Size_Size_Id",
                table: "Product",
                column: "Size_Id",
                principalTable: "Size",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Size_Size_Id",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_Size_Id",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Size_Id",
                table: "Product");
        }
    }
}
