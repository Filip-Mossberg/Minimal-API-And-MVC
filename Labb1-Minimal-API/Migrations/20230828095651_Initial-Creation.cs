using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Labb1_Minimal_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvalible = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Released = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "book",
                columns: new[] { "Id", "Author", "Description", "Genre", "IsAvalible", "Released", "Title" },
                values: new object[,]
                {
                    { 1, "Filip", "Horror book based on teenagers getting lost in the deep forest.", "Horror", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Night" },
                    { 2, "Anas", "Fighting for survival, one man standing.", "Thriller", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hungergames" },
                    { 3, "Ulrika", "Island Zoo full of dinosours, but things get out of hand.", "Action", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Urasic Park" },
                    { 4, "Isabella", "Explorer tries to get rid off big white shark that is causing problems for residents.", "Horror", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Meg" },
                    { 5, "Joakim", "The good verses the evel fight over the galaxy.", "Science fiction", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "book");
        }
    }
}
