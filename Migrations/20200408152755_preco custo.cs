using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFaturamentoJnx.Migrations
{
    public partial class precocusto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrecoCusto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Peso = table.Column<double>(nullable: false),
                    Trigo = table.Column<double>(nullable: false),
                    Embalagem = table.Column<double>(nullable: false),
                    Caixa = table.Column<double>(nullable: false),
                    Folha = table.Column<double>(nullable: false),
                    Vitamina = table.Column<double>(nullable: false),
                    Lecitina = table.Column<double>(nullable: false),
                    Fita = table.Column<double>(nullable: false),
                    Bandeja = table.Column<double>(nullable: false),
                    Energia = table.Column<double>(nullable: false),
                    Lenha = table.Column<double>(nullable: false),
                    Manutenção = table.Column<double>(nullable: false),
                    PorcentagemVendedor = table.Column<double>(nullable: false),
                    PorcentagemLucro = table.Column<double>(nullable: false),
                    CustoFixo = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrecoCusto", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrecoCusto");
        }
    }
}
