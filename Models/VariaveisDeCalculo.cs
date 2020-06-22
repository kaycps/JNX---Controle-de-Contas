using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFaturamentoJnx.Models
{
    public class VariaveisDeCalculo
    {
        public int Id { get; set; }
        public double Pis { get; set; }
        public double Confins { get; set; }
        public double Fgts { get; set; }
        public double Inss { get; set; }
        public double ComissaoVendedor { get; set; }
        public double Icms { get; set; }
        public double FreteVenda { get; set; }
        public double FreteCompra { get; set; }
        public int ProducaoMensal { get; set; }

    }
}
