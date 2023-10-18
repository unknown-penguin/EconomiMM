using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EconomiMM.Migrations
{
    public partial class AddJointMaterialTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JointMaterialTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JointMaterialTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpansionJointsMaterials",
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
                    table.PrimaryKey("PK_ExpansionJointsMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpansionJointsMaterials_JointMaterialTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "JointMaterialTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpansionJointsMaterials_TypeId",
                table: "ExpansionJointsMaterials",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpansionJointsMaterials");

            migrationBuilder.DropTable(
                name: "JointMaterialTypes");
        }
    }
}
