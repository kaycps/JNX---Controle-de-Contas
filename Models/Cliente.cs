using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFaturamentoJnx.Models
{
    public class Cliente
    {
               
        public int ID { get; set; }

        public string Documento { get; set; }

        public string NumeroDocumento { get; set; }
        
        public string Nome { get; set; }
        
        public string Telefone { get; set; }

        public  ICollection<Fatura> Faturas { get; set;}

        public  Endereco Endereco { get; set; }

    }
}
