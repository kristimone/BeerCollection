using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeerCollection.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BeerRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    BeerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeerRatings_Beers_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Pale Ale", "Ale" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Stout", "Dark" }
                });

            migrationBuilder.InsertData(
                table: "BeerRatings",
                columns: new[] { "Id", "BeerId", "Value" },
                values: new object[] { 1, new Guid("11111111-1111-1111-1111-111111111111"), 4 });

            migrationBuilder.CreateIndex(
                name: "IX_BeerRatings_BeerId",
                table: "BeerRatings",
                column: "BeerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeerRatings");

            migrationBuilder.DropTable(
                name: "Beers");
        }
    }
}
