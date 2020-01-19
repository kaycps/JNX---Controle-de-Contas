using ControleFaturamentoJnx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFaturamentoJnx.Data.DAO
{
    
    public class FaturaDao
    {
        private ControleFaturamentoContext _context;

        public FaturaDao(ControleFaturamentoContext context)
        {
            _context = context;

        }

        public List<Fatura>  BuscarAllFaturas()
        {
            List<Fatura> faturas = new List<Fatura>();

            foreach(var fatura in _context.Faturas)
            {
                faturas.Add(fatura );

            }
            return faturas;
        }

    }
}
