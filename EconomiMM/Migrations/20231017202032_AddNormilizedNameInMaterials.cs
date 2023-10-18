using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EconomiMM.Migrations
{
    public partial class AddNormilizedNameInMaterials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "normilized_name",
                table: "LinerMaterials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "normilized_name",
                table: "FlangeMaterials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "normilized_name",
                table: "ExpansionJointsMaterials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "normilized_name",
                table: "LinerMaterials");

            migrationBuilder.DropColumn(
                name: "normilized_name",
                table: "FlangeMaterials");

            migrationBuilder.DropColumn(
                name: "normilized_name",
                table: "ExpansionJointsMaterials");
        }
    }
}
