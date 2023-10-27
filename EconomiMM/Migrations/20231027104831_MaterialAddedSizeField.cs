using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EconomiMM.Migrations
{
    public partial class MaterialAddedSizeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Material",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Material");
        }
    }
}
