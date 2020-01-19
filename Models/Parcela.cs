using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFaturamentoJnx.Models
{
    public class Parcela
    {

        public int ID { get; set; }
        
        public double ValorParcela { get; set; }
        public DateTime Vencimento { get; set; }
        public string Status { get; set; }
             
        public int FaturaID { get; set; }
        public Fatura Fatura { get; set; }


    }
}
