using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFaturamentoJnx.Models
{
    public class Fatura
    {
        public int ID { get; set; }
        [Required]
        public DateTime DataEmissao { get; set; }        
        public string Tipo { get; set; }
        public string Numero  { get; set; }
        [Required]
        public int NumeroParcelas { get; set; }
        [Required]
        public Double ValorFatura { get; set; }
        public string Observacao { get; set; }
        
        public int ClienteID { get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<Parcela> Parcelas { get; set; }


        

    }
}
