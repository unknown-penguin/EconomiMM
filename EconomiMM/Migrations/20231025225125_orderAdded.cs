using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EconomiMM.Migrations
{
    public partial class orderAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    temperature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jointShape = table.Column<int>(type: "int", nullable: false),
                    jointType = table.Column<int>(type: "int", nullable: false),
                    withLiner = table.Column<bool>(type: "bit", nullable: false),
                    mainPartLength1 = table.Column<float>(type: "real", nullable: true),
                    mainPartDiameter = table.Column<float>(type: "real", nullable: true),
                    mainPartLength2 = table.Column<float>(type: "real", nullable: false),
                    mainPartWidth = table.Column<float>(type: "real", nullable: false),
                    flangeWidth = table.Column<float>(type: "real", nullable: false),
                    koef = table.Column<float>(type: "real", nullable: false),
                    siliconeTubes = table.Column<int>(type: "int", nullable: false),
                    linerPartLength = table.Column<float>(type: "real", nullable: false),
                    linerPartHeight = table.Column<float>(type: "real", nullable: false),
                    linerPartWidth = table.Column<float>(type: "real", nullable: false),
                    linerPartBindingWidth = table.Column<float>(type: "real", nullable: false),
                    mainPartWorkPrice = table.Column<float>(type: "real", nullable: false),
                    linerPartWorkPrice = table.Column<float>(type: "real", nullable: false),
                    TotalPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SelectedMaterial<ExpansionJointMaterial>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedMaterial<ExpansionJointMaterial>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectedMaterial<ExpansionJointMaterial>_ExpansionJointsMaterials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "ExpansionJointsMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SelectedMaterial<ExpansionJointMaterial>_Orders_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SelectedMaterial<FlangeMaterial>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedMaterial<FlangeMaterial>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectedMaterial<FlangeMaterial>_FlangeMaterials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "FlangeMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SelectedMaterial<FlangeMaterial>_Orders_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SelectedMaterial<LinerExpansionJointMaterial>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedMaterial<LinerExpansionJointMaterial>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectedMaterial<LinerExpansionJointMaterial>_LinerMaterials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "LinerMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SelectedMaterial<LinerExpansionJointMaterial>_Orders_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelectedMaterial<ExpansionJointMaterial>_MaterialId",
                table: "SelectedMaterial<ExpansionJointMaterial>",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedMaterial<ExpansionJointMaterial>_ProductId",
                table: "SelectedMaterial<ExpansionJointMaterial>",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedMaterial<FlangeMaterial>_MaterialId",
                table: "SelectedMaterial<FlangeMaterial>",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedMaterial<FlangeMaterial>_ProductId",
                table: "SelectedMaterial<FlangeMaterial>",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedMaterial<LinerExpansionJointMaterial>_MaterialId",
                table: "SelectedMaterial<LinerExpansionJointMaterial>",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedMaterial<LinerExpansionJointMaterial>_ProductId",
                table: "SelectedMaterial<LinerExpansionJointMaterial>",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectedMaterial<ExpansionJointMaterial>");

            migrationBuilder.DropTable(
                name: "SelectedMaterial<FlangeMaterial>");

            migrationBuilder.DropTable(
                name: "SelectedMaterial<LinerExpansionJointMaterial>");

            migrationBuilder.DropTable(
                name: "Orders");

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
    }
}
