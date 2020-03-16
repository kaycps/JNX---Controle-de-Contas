using ControleFaturamentoJnx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFaturamentoJnx.Views.Shared
{
    public class GerarPdfParcela
    {

        public GerarPdfParcela()
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
            
            textFormatter.DrawString("Relatorio De Parcelas - JNX", tituloDetalhes, corFonte, new PdfSharpCore.Drawing.XRect(
                                    30, 75, Page.Width, Page.Height));
            
            textFormatter.DrawString("Periodo: " + DataInicial.Date.ToString("d") + " até " + DataFinal.Date.ToString("d")
                                    , fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                    30, 100, Page.Width, Page.Height));
        }
        public void EscreverParcelaPagasTags()
        {
            var corFonte = PdfSharpCore.Drawing.XBrushes.Black;
            var fonteTag = new PdfSharpCore.Drawing.XFont("Arial", 10, PdfSharpCore.Drawing.XFontStyle.Bold);
            var fonteTipo = new PdfSharpCore.Drawing.XFont("Arial", 12, PdfSharpCore.Drawing.XFontStyle.Bold);


            textFormatter.DrawString("Pagos", fonteTipo, corFonte, new PdfSharpCore.Drawing.XRect(
                                        30, 150, Page.Width, Page.Height));

            textFormatter.DrawString("Id", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    30, 180, Page.Width, Page.Height));

            textFormatter.DrawString("Valor Da Parcela", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    100, 180, Page.Width, Page.Height));

            textFormatter.DrawString("Vencimento", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    200, 180, Page.Width, Page.Height));

            textFormatter.DrawString("Cliente", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    300, 180, Page.Width, Page.Height));

            textFormatter.DrawString("Nota", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    400, 180, Page.Width, Page.Height));
        }
        public void EscreverParcelaPagasCorpo(Parcela item, int Altura)
        {

            var fonteConteudo = new PdfSharpCore.Drawing.XFont("Arial", 10);
            var corFonte = PdfSharpCore.Drawing.XBrushes.Black;

            textFormatter.DrawString(item.ID.ToString(), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                        30, Altura, Page.Width, Page.Height));
            textFormatter.DrawString("R$ " + item.ValorParcela.ToString() , fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                100, Altura, Page.Width, Page.Height));
            textFormatter.DrawString(item.Vencimento.Date.ToString("d"), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                200, Altura, Page.Width, Page.Height));
            if (item.Fatura.Cliente != null)
            {
                textFormatter.DrawString(item.Fatura.Cliente.Nome.ToString(), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                300, Altura, Page.Width, Page.Height));
            }
            if (item.Fatura.Numero != null)
            {
                textFormatter.DrawString(item.Fatura.Numero.ToString(), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                400, Altura, Page.Width, Page.Height));
            }
            

        }
        public void EscreverParcelaPendenteTags(int Altura)
        {
            var corFonte = PdfSharpCore.Drawing.XBrushes.Black;
            var fonteTag = new PdfSharpCore.Drawing.XFont("Arial", 10, PdfSharpCore.Drawing.XFontStyle.Bold);
            var fonteTipo = new PdfSharpCore.Drawing.XFont("Arial", 12, PdfSharpCore.Drawing.XFontStyle.Bold);


            textFormatter.DrawString("Pendentes", fonteTipo, corFonte, new PdfSharpCore.Drawing.XRect(
                                       30, Altura +20, Page.Width, Page.Height));

            textFormatter.DrawString("Id", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    30, Altura + 50, Page.Width, Page.Height));
            textFormatter.DrawString("Valor Da Parcela", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    100, Altura + 50, Page.Width, Page.Height));
            textFormatter.DrawString("Vencimento", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    200, Altura + 50, Page.Width, Page.Height));
            textFormatter.DrawString("Cliente", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    300, Altura + 50, Page.Width, Page.Height));
            textFormatter.DrawString("Nota", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    400, Altura + 50, Page.Width, Page.Height));
        }
        public void EscreverParcelaPendenteCorpo(Parcela item, int Altura)
        {
            var fonteConteudo = new PdfSharpCore.Drawing.XFont("Arial", 10);
            var corFonte = PdfSharpCore.Drawing.XBrushes.Black;

            textFormatter.DrawString(item.ID.ToString(), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                        30, Altura+70, Page.Width, Page.Height));
            textFormatter.DrawString("R$ " + item.ValorParcela.ToString() , fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                100, Altura + 70, Page.Width, Page.Height));
            textFormatter.DrawString(item.Vencimento.Date.ToString("d"), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                200, Altura + 70, Page.Width, Page.Height));
            if (item.Fatura.Cliente != null)
            {
                textFormatter.DrawString(item.Fatura.Cliente.Nome.ToString(), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                300, Altura + 70, Page.Width, Page.Height));
            }

            if (item.Fatura.Numero != null)
            {
                textFormatter.DrawString(item.Fatura.Numero.ToString(), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                400, Altura + 70, Page.Width, Page.Height));
            }
            

        }
        public void EscreverParcelaVencidaTags(int Altura)
        {
            var corFonte = PdfSharpCore.Drawing.XBrushes.Black;
            var fonteTag = new PdfSharpCore.Drawing.XFont("Arial", 10, PdfSharpCore.Drawing.XFontStyle.Bold);
            var fonteTipo = new PdfSharpCore.Drawing.XFont("Arial", 12, PdfSharpCore.Drawing.XFontStyle.Bold);

            textFormatter.DrawString("Vencidos", fonteTipo, corFonte, new PdfSharpCore.Drawing.XRect(
                                       30, Altura + 90, Page.Width, Page.Height));

            textFormatter.DrawString("Id", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    30, Altura + 120, Page.Width, Page.Height));
            textFormatter.DrawString("Valor Da Parcela", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    100, Altura + 120, Page.Width, Page.Height));
            textFormatter.DrawString("Vencimento", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    200, Altura + 120, Page.Width, Page.Height));
            textFormatter.DrawString("Cliente", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    300, Altura + 120, Page.Width, Page.Height));
            textFormatter.DrawString("Nota", fonteTag, corFonte, new PdfSharpCore.Drawing.XRect(
                                    400, Altura + 120, Page.Width, Page.Height));
        }
        public void EscreverParcelaVencidaCorpo(Parcela item, int Altura)
        {
            var fonteConteudo = new PdfSharpCore.Drawing.XFont("Arial", 10);
            var corFonte = PdfSharpCore.Drawing.XBrushes.Black;

            textFormatter.DrawString(item.ID.ToString(), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                      30, Altura+140, Page.Width, Page.Height));
            textFormatter.DrawString("R$ " + item.ValorParcela.ToString() , fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                100, Altura + 140, Page.Width, Page.Height));
            textFormatter.DrawString(item.Vencimento.Date.ToString("d"), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                200, Altura + 140, Page.Width, Page.Height));
            textFormatter.DrawString(item.Fatura.Cliente.Nome.ToString(), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                300, Altura + 140, Page.Width, Page.Height));
            textFormatter.DrawString(item.Fatura.Numero.ToString(), fonteConteudo, corFonte, new PdfSharpCore.Drawing.XRect(
                                400, Altura + 140, Page.Width, Page.Height));
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
