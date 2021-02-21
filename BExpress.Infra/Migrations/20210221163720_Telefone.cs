using Microsoft.EntityFrameworkCore.Migrations;

namespace BExpress.Infra.Migrations
{
    public partial class Telefone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Usuarios");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Pessoas",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Pessoas");

            migrationBuilder.AddColumn<string>(
                name: "Imagem",
                table: "Usuarios",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
