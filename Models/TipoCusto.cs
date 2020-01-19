using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFaturamentoJnx.Models
{
    public class TipoCusto
    {
        public int ID { get; set; }
        public string Nome { get; set; }

        public ICollection<Despesa> Despesas { get; set; }
    }
}
