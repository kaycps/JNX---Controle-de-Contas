using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFaturamentoJnx.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Documento = table.Column<string>(nullable: true),
                    NumeroDocumento = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TipoCusto",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCusto", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Estado = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Rua = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    Cep = table.Column<string>(nullable: true),
                    ClienteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Enderecos_Clientes_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Clientes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Faturas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataEmissao = table.Column<DateTime>(nullable: false),
                    Tipo = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    NumeroParcelas = table.Column<int>(nullable: false),
                    ValorFatura = table.Column<double>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    ClienteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faturas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Faturas_Clientes_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Clientes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Despesa",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoCustoID = table.Column<int>(nullable: false),
                    Valor = table.Column<double>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Observacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesa", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Despesa_TipoCusto_TipoCustoID",
                        column: x => x.TipoCustoID,
                        principalTable: "TipoCusto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parcelas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ValorParcela = table.Column<double>(nullable: false),
                    Vencimento = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    FaturaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcelas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Parcelas_Faturas_FaturaID",
                        column: x => x.FaturaID,
                        principalTable: "Faturas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_TipoCustoID",
                table: "Despesa",
                column: "TipoCustoID");

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_ClienteID",
                table: "Enderecos",
                column: "ClienteID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faturas_ClienteID",
                table: "Faturas",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Parcelas_FaturaID",
                table: "Parcelas",
                column: "FaturaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Despesa");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Parcelas");

            migrationBuilder.DropTable(
                name: "TipoCusto");

            migrationBuilder.DropTable(
                name: "Faturas");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
