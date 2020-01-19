using ControleFaturamentoJnx.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFaturamentoJnx.ViewModels
{
    public class CaixaVM
    {

        private ControleFaturamentoContext _context;

        public CaixaVM(ControleFaturamentoContext context)
        {
            _context = context;
        }
    }
}
