using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFaturamentoJnx.Models
{
    public class Producao
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int Quantidade { get; set; }
        public string Tipo { get; set; }
        public string Peso { get; set; }

    }
}
