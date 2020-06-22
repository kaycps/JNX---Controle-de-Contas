using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFaturamentoJnx.Migrations
{
    public partial class VariaveisdeCalculo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VariaveisDeCalculo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Pis = table.Column<double>(nullable: false),
                    Confins = table.Column<double>(nullable: false),
                    Fgts = table.Column<double>(nullable: false),
                    Inss = table.Column<double>(nullable: false),
                    ComissaoVendedor = table.Column<double>(nullable: false),
                    Icms = table.Column<double>(nullable: false),
                    FreteVenda = table.Column<double>(nullable: false),
                    FreteCompra = table.Column<double>(nullable: false),
                    ProducaoMensal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariaveisDeCalculo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VariaveisDeCalculo");
        }
    }
}
