using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ControleFaturamentoJnx.Models;
using ControleFaturamentoJnx.Data;
using System.IO;
using Microsoft.EntityFrameworkCore;
using ControleFaturamentoJnx.Views.Shared.Enum;
using ControleFaturamentoJnx.Views.Shared;

namespace ControleFaturamentoJnx.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ControleFaturamentoContext _context;
        public HomeController(ILogger<HomeController> logger, ControleFaturamentoContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            
            //Buscando os valores ganhos no mes e os valores ganhos em total
            var ValorParcelasMesPago = _context.Parcelas
                             .Where(p => p.Status.Equals("Pago") && p.Vencimento.Month.Equals(DateTime.Now.Month) &&
                             p.Vencimento.Year.Equals(DateTime.Now.Year)).ToList();
            int c = 0;
            int TotalParcelasVencidasMes = _context.Parcelas
                             .Where(p => p.Status.Equals("Vencido") && p.Vencimento.Month.Equals(DateTime.Now.Month) &&
                             p.Vencimento.Year.Equals(DateTime.Now.Year) && p.Vencimento.Day.CompareTo(DateTime.Now.Day)<c).Count();

            var ValorParcelasTotalPago = _context.Parcelas.Where(p=>p.Status.Equals("Pago")).ToList();

            var ValorParcelaMesPendente = _context.Parcelas
                             .Where(p => p.Status.Equals("Pendente") && p.Vencimento.Month.Equals(DateTime.Now.Month) &&
                             p.Vencimento.Year.Equals(DateTime.Now.Year));

            var ValorParcelaTotalPendente = _context.Parcelas.Where(p => p.Status.Equals("Pendente")).ToList();

            // faturamento
            var FaturamentoMes = _context.Faturas.Where(f => f.DataEmissao.Month.Equals(DateTime.Now.Month)
                                                        &&f.DataEmissao.Year.Equals(DateTime.Now.Year));

            var FaturamentoTotal = _context.Faturas.Select(f=>f.ValorFatura).Sum();

            // despesas

            var DespesaMes = _context.Despesa.Where(d => d.Data.Month.Equals(DateTime.Now.Month)
                                                    && d.Data.Year.Equals(DateTime.Now.Year));

            var DespesaTotal = _context.Despesa.Select(d => d.Valor).Sum();

            double valorMesPago = ValorParcelasMesPago.Sum(p=>p.ValorParcela), 
                valorMesTotalPAgo = ValorParcelasTotalPago.Sum(p => p.ValorParcela), 
                valorMesPendente = ValorParcelaMesPendente.Sum(p => p.ValorParcela), 
                valorMesTotalPendente = ValorParcelaTotalPendente.Sum(p => p.ValorParcela);          

            ViewBag.ParcelaMesPago = valorMesPago;
            ViewBag.ParcelaTotalPago = valorMesTotalPAgo;
            ViewBag.ParcelaMesPendente = valorMesPendente;
            ViewBag.ParcelaTotalPendente = valorMesTotalPendente;
            ViewBag.FaturamentoMes = FaturamentoMes;
            ViewBag.FaturamentoTotal = FaturamentoTotal;
            ViewBag.DepesaMes = DespesaMes;
            ViewBag.DespesaTotal = DespesaTotal;
            ViewBag.Lucro = FaturamentoTotal - DespesaTotal;
            ViewBag.ParcelasVencidasMes = TotalParcelasVencidasMes;

            return View();
        }

        public IActionResult Contato()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public FileResult RelatorioCaixa(DateTime DataInicial, DateTime DataFinal)
        {
            
           using (var doc = new PdfSharpCore.Pdf.PdfDocument())
            {                  
                                
                //Query Faturas
                var Faturamento =_context.Faturas.Include(f=>f.Cliente).Where(f => f.DataEmissao.Date >= DataInicial.Date 
                                                        && f.DataEmissao.Date <= DataFinal.Date).AsNoTracking()
                                                        .OrderBy(f=>f.DataEmissao).ToList();
                //Query Despesas
                var Despesa = _context.Despesa.Include(d=>d.TipoCusto).Where(f => f.Data.Date >= DataInicial.Date
                                                        && f.Data.Date <= DataFinal.Date).AsNoTracking()
                                                        .OrderBy(f => f.Data).ToList();
                //Query Saldo
                double Saldo = _context.Faturas.Where(f => f.DataEmissao.Date >= DataInicial.Date
                                                        && f.DataEmissao.Date <= DataFinal.Date)
                                                      .Select(f=>f.ValorFatura).Sum()
                               - _context.Despesa.Where(f => f.Data.Date >= DataInicial.Date
                                                        && f.Data.Date <= DataFinal.Date)
                                                       .Select(f=>f.Valor).Sum();

                GerarPdfCaixa gerarPdf = new GerarPdfCaixa();
                int Altura = 200;
                int Linhas = 0;

                gerarPdf.AddPagina();
                gerarPdf.EscreverHead(DataInicial,DataFinal);
                gerarPdf.EscreverFaturamentoTags();
                gerarPdf.EscreverNumeroPagina();

                foreach (var item in Faturamento)
                {
                    gerarPdf.EscreverFaturamentoCorpo(item, Altura);
                    Linhas++;
                    Altura += 20;

                    if (Linhas>22)
                    {
                        gerarPdf.AddPagina();
                        Linhas = 0;
                        Altura = 200;
                        gerarPdf.EscreverHead(DataInicial, DataFinal);
                        gerarPdf.EscreverFaturamentoTags();
                        gerarPdf.EscreverNumeroPagina();
                    }
                }

                gerarPdf.EscreverDespesaTags(Altura);
                foreach (var item in Despesa)
                {
                    gerarPdf.EscreverDespesaCorpo(item,Altura);
                    Linhas++;
                    Altura += 20;

                    if (Linhas > 22)
                    {
                        gerarPdf.AddPagina();
                        Linhas = 0;
                        Altura = 150;
                        gerarPdf.EscreverHead(DataInicial, DataFinal);
                        gerarPdf.EscreverDespesaTags(Altura);
                        gerarPdf.EscreverNumeroPagina();
                    }
                }

                gerarPdf.EscreverSaldo(Saldo,Altura);

                using (MemoryStream stream = new MemoryStream())
                {
                    var contantType = "application/pdf";

                    gerarPdf._document.Save(stream, false);

                    var nomeArquivo = "Relatorio_Parcelas" + DataInicial.Date.ToString("d") + "_" + DataFinal.Date.ToString("d") + ".pdf";

                    return File(stream.ToArray(), contantType, nomeArquivo);
                }

            }
        }

        public FileResult RelatorioParcela(DateTime DataInicial, DateTime DataFinal)
        {
            using (var doc = new PdfSharpCore.Pdf.PdfDocument())
            {
                                
                //Query Parcelas Pagas
                var ParcelasPagas = _context.Parcelas.Include(f => f.Fatura.Cliente).Where(f => f.Fatura.DataEmissao.Date >= DataInicial.Date
                                                           && f.Fatura.DataEmissao.Date <= DataFinal.Date
                                                           && f.Status.Equals("Pago")).AsNoTracking().OrderBy(f => f.Vencimento).ToList();
                //Query Parcelas Vencidas
                var ParcelasVencidas = _context.Parcelas.Include(f => f.Fatura.Cliente).Where(f => f.Fatura.DataEmissao.Date >= DataInicial.Date
                                                          && f.Fatura.DataEmissao.Date <= DataFinal.Date
                                                          && f.Status.Equals("Vencido")).AsNoTracking().OrderBy(f=>f.Vencimento).ToList();
                //Query Parcelas Pendente
                var ParcelasPendente = _context.Parcelas.Include(f => f.Fatura.Cliente).Where(f => f.Fatura.DataEmissao.Date >= DataInicial.Date
                                                          && f.Fatura.DataEmissao.Date <= DataFinal.Date
                                                          && f.Status.Equals("Pendente")).AsNoTracking().OrderBy(f => f.Vencimento).ToList();

                GerarPdfParcela gerarPdf = new GerarPdfParcela();
                int Altura = 200;
                int Linhas = 0;

                gerarPdf.AddPagina();
                gerarPdf.EscreverHead(DataInicial, DataFinal);
                gerarPdf.EscreverParcelaPagasTags();
                gerarPdf.EscreverNumeroPagina();

                foreach (var item in ParcelasPagas)
                {
                    gerarPdf.EscreverParcelaPagasCorpo(item, Altura);
                    Linhas++;
                    Altura += 20;

                    if (Linhas > 22)
                    {
                        gerarPdf.AddPagina();
                        Linhas = 0;
                        Altura = 200;
                        gerarPdf.EscreverHead(DataInicial, DataFinal);
                        gerarPdf.EscreverParcelaPagasTags();
                        gerarPdf.EscreverNumeroPagina();
                    }
                }

                gerarPdf.EscreverParcelaPendenteTags(Altura);

                foreach (var item in ParcelasPendente)
                {
                    gerarPdf.EscreverParcelaPendenteCorpo(item, Altura);
                    Linhas++;
                    Altura += 20;

                    if (Linhas > 22)
                    {
                        gerarPdf.AddPagina();
                        Linhas = 0;
                        Altura = 200;
                        gerarPdf.EscreverHead(DataInicial, DataFinal);
                        gerarPdf.EscreverParcelaPendenteTags(Altura);
                        gerarPdf.EscreverNumeroPagina();
                    }
                }

                gerarPdf.EscreverParcelaVencidaTags(Altura);

                foreach (var item in ParcelasVencidas)
                {
                    gerarPdf.EscreverParcelaVencidaCorpo(item, Altura);
                    Linhas++;
                    Altura += 20;

                    if (Linhas > 22)
                    {
                        gerarPdf.AddPagina();
                        Linhas = 0;
                        Altura = 200;
                        gerarPdf.EscreverHead(DataInicial, DataFinal);
                        gerarPdf.EscreverParcelaVencidaTags(Altura);
                        gerarPdf.EscreverNumeroPagina();
                    }
                }


                using (MemoryStream stream = new MemoryStream())
                {
                    var contantType = "application/pdf";

                    gerarPdf._document.Save(stream, false);

                    var nomeArquivo = "Relatorio_Parcelas" + DataInicial.Date.ToString("d") + "_" + DataFinal.Date.ToString("d") + ".pdf";

                    return File(stream.ToArray(), contantType, nomeArquivo);
                }

            }

        }
    }
}
