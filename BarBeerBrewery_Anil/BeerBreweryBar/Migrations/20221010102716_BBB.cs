using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerBreweryBar.Migrations
{
    public partial class BBB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Address = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brewery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brewery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Beer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    PercentageAlcoholByVolume = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    BreweryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beer_Brewery_BreweryId",
                        column: x => x.BreweryId,
                        principalTable: "Brewery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BarBeers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BarId = table.Column<int>(nullable: false),
                    BeerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarBeers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BarBeers_Bar_BarId",
                        column: x => x.BarId,
                        principalTable: "Bar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarBeers_Beer_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarBeers_BarId",
                table: "BarBeers",
                column: "BarId");

            migrationBuilder.CreateIndex(
                name: "IX_BarBeers_BeerId",
                table: "BarBeers",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_Beer_BreweryId",
                table: "Beer",
                column: "BreweryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarBeers");

            migrationBuilder.DropTable(
                name: "Bar");

            migrationBuilder.DropTable(
                name: "Beer");

            migrationBuilder.DropTable(
                name: "Brewery");
        }
    }
}
