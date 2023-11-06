using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EconomiMM.Migrations
{
    public partial class addNewPriceFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DealerPriceForSheet",
                table: "Material",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DealerPriceForSqMetre",
                table: "Material",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OurPriceForSheet",
                table: "Material",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OurPriceForSqMetre",
                table: "Material",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DealerPriceForSheet",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "DealerPriceForSqMetre",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "OurPriceForSheet",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "OurPriceForSqMetre",
                table: "Material");
        }
    }
}
