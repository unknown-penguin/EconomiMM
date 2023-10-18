using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EconomiMM.Migrations
{
    public partial class AddOtherMaterialsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlangeMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Thickness = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlangeMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlangeMaterials_JointMaterialTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "JointMaterialTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LinerMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Thickness = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    PartOfLiner = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinerMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinerMaterials_JointMaterialTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "JointMaterialTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlangeMaterials_TypeId",
                table: "FlangeMaterials",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LinerMaterials_TypeId",
                table: "LinerMaterials",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlangeMaterials");

            migrationBuilder.DropTable(
                name: "LinerMaterials");
        }
    }
}
