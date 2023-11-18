using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EconomiMM.Migrations
{
    public partial class addPriceKoefs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceKoeficients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OurPriceForSheetKoef = table.Column<float>(type: "real", nullable: false),
                    OurPriceForSqMetreKoef = table.Column<float>(type: "real", nullable: false),
                    DealerPriceForSheetKoef = table.Column<float>(type: "real", nullable: false),
                    DealerPriceForSqMetreKoef = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceKoeficients", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceKoeficients");
        }
    }
}
