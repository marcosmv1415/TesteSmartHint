using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteSmartHint.Migrations
{
    public partial class ColunaFalhaNome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FalhaNome",
                table: "Verificacoes",
                type: "longtext",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FalhaNome",
                table: "Verificacoes");
        }
    }
}
