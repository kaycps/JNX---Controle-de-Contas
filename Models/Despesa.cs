using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFaturamentoJnx.Models
{
    public class Despesa
    {

        public int ID { get; set; }
        public int TipoCustoID { get; set; }
        public TipoCusto TipoCusto { get; set; }
        public Double Valor { get; set; }
        public DateTime Data { get; set; }
        public string Observacao { get; set; }



    }
}
