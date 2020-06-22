using ControleFaturamentoJnx.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFaturamentoJnx.Views.Shared.Enum
{
    public class GerarPdfCaixa    {        
       

        public GerarPdfCaixa()
        {
            
            _document = new PdfSharpCore.Pdf.PdfDocument();
            
        }

        public PdfSharpCore.Drawing.XGraphics Gfx { get; set; }
        public PdfSharpCore.Pdf.PdfPage Page { get; set; }
        public PdfSharpCore.Drawing.Layout.XTextFormatter textFormatter { get; set; }
        public PdfSharpCore.Pdf.PdfDocument _document { get; set; }

        public void AddPagina()
        {
            Page = _document.AddPage();
            Gfx = PdfSharpCore.Drawing.XGraphics.FromPdfPage(Page);
            textFormatter = new PdfSharpCore.Drawing.Layout.XTextFormatter(Gfx);
        }
        public void EscreverHead(DateTime DataInicial, DateTime DataFinal)
        {
            var corFonte = PdfSharpCore.Drawing.XBrushes.Black;                     
            var fonteConteudo = new PdfSharpCore.Drawing.XFont("Arial", 10);            
            var tituloDetalhes = new PdfSharpCore.Drawing.XFont("Arial", 14, PdfSharpCore.Drawing.XFontStyle.Bold);           

            textFormatter.DrawString("Fluxo de Caixa - JNX", tituloDetalhes, corFonte, new PdfSharpCore.Drawing.XRect(
                                        30, 75, Page.Width, Page.Height));
            textFormatter.DrawString("Periodo: " + DataInicial.Date.ToString("d") + " até " + DataFinal.Date.ToString("d")
                                        , fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                        30, 100, Page.Width, Page.Height));
        }
        public void EscreverFaturamentoTags()
        {
            var corFonte = PdfSharpCore.Drawing.XBrushes.Black;
            var fonteTag = new PdfSharpCore.Drawing.XFont("Arial", 10, PdfSharpCore.Drawing.XFontStyle.Bold);
            var fonteTipo = new PdfSharpCore.Drawing.XFont("Arial", 12, PdfSharpCore.Drawing.XFontStyle.Bold);
            

            textFormatter.DrawString("Faturamento", fonteTipo, corFonte, new PdfSharpCore.Drawing.XRect(
                                        30, 150, Page.Width, Page.Height));
            textFormatter.DrawString("Numero", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    30, 180, Page.Width, Page.Height));
            textFormatter.DrawString("Valor", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    100, 180, Page.Width, Page.Height));
            textFormatter.DrawString("Data", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    200, 180, Page.Width, Page.Height));
            textFormatter.DrawString("Cliente", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    300, 180, Page.Width, Page.Height));
            textFormatter.DrawString("Obs", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    400, 180, Page.Width, Page.Height));
        }
        public void EscreverFaturamentoCorpo(Fatura item, int Altura)
        {
            
            var fonteConteudo = new PdfSharpCore.Drawing.XFont("Arial", 10);
            var corFonte = PdfSharpCore.Drawing.XBrushes.Black;
            
                          
                textFormatter.DrawString(item.Numero.ToString(), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                    30, Altura, Page.Width, Page.Height));
                textFormatter.DrawString("R$ " + item.ValorFatura.ToString() + ",00", fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                    100, Altura, Page.Width, Page.Height));
                textFormatter.DrawString(item.DataEmissao.Date.ToString("d"), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                    200, Altura, Page.Width, Page.Height));
                textFormatter.DrawString(item.Cliente.Nome.ToString(), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                    300, Altura, Page.Width, Page.Height));
                if (item.Observacao != null)
                {
                    textFormatter.DrawString(item.Observacao.ToString(), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                    400, Altura, Page.Width, Page.Height));
                }
                               
            
        }
        public void EscreverDespesaTags(int Altura)
        {
            var corFonte = PdfSharpCore.Drawing.XBrushes.Black;
            var fonteTag = new PdfSharpCore.Drawing.XFont("Arial", 10, PdfSharpCore.Drawing.XFontStyle.Bold);
            var fonteTipo = new PdfSharpCore.Drawing.XFont("Arial", 12, PdfSharpCore.Drawing.XFontStyle.Bold);
            

            textFormatter.DrawString("Despesas", fonteTipo, corFonte, new PdfSharpCore.Drawing.XRect(
                                        30, Altura + 20, Page.Width, Page.Height));
            textFormatter.DrawString("ID", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    30, Altura + 50, Page.Width, Page.Height));
            textFormatter.DrawString("Valor", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    100, Altura + 50, Page.Width, Page.Height));
            textFormatter.DrawString("Data", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    200, Altura + 50, Page.Width, Page.Height));
            textFormatter.DrawString("Tipo", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    300, Altura + 50, Page.Width, Page.Height));
            textFormatter.DrawString("Obs", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    400, Altura + 50, Page.Width, Page.Height));
        }
        public void EscreverDespesaCorpo(Despesa item, int Altura)
        {
            var fonteConteudo = new PdfSharpCore.Drawing.XFont("Arial", 10);
            var corFonte = PdfSharpCore.Drawing.XBrushes.Black;

            textFormatter.DrawString(item.ID.ToString(), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                       30, Altura+70, Page.Width, Page.Height));
            textFormatter.DrawString("R$ " + item.Valor.ToString() + ",00", fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                        100, Altura + 70, Page.Width, Page.Height));
            textFormatter.DrawString(item.Data.Date.ToString("d"), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                        200, Altura + 70, Page.Width, Page.Height));
            textFormatter.DrawString(item.TipoCusto.Nome.ToString(), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                        300, Altura + 70, Page.Width, Page.Height));
            if (item.Observacao != null)
            {
                textFormatter.DrawString(item.Observacao.ToString(), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                         400, Altura + 70, Page.Width, Page.Height));
            }
            
        }
        public void EscreverSaldo(double Saldo,int Altura)
        {
            var fonteConteudo = new PdfSharpCore.Drawing.XFont("Arial", 10);
            var corFonte = PdfSharpCore.Drawing.XBrushes.Black;
            var fonteTipo = new PdfSharpCore.Drawing.XFont("Arial", 12, PdfSharpCore.Drawing.XFontStyle.Bold);

            textFormatter.DrawString("Saldo: ", fonteTipo, corFonte, new PdfSharpCore.Drawing.XRect(
                                        30, Altura + 90, Page.Width, Page.Height));
            textFormatter.DrawString(Saldo.ToString(), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                    80, Altura + 90, Page.Width, Page.Height));
        }        
        public void EscreverNumeroPagina()
        {
            int nPaginas = _document.PageCount;
            var corFonte = PdfSharpCore.Drawing.XBrushes.Black;

            textFormatter.DrawString(nPaginas.ToString(), new PdfSharpCore.Drawing.XFont("Arial", 10), corFonte,
                                    new PdfSharpCore.Drawing.XRect(578, 825, Page.Width, Page.Height));
        }


    }
}
