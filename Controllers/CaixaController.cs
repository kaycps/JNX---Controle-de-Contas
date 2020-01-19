using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFaturamentoJnx.Data;
using ControleFaturamentoJnx.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleFaturamentoJnx.Controllers
{
    public class CaixaController : Controller
    {
        private ControleFaturamentoContext _context;
        
        

        public CaixaController(ControleFaturamentoContext context)
        {
            _context = context;
            
        }

        public IActionResult Index()
        {
            
                        

            return View();
        }
    }
}