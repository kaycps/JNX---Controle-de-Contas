using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleFaturamentoJnx.Data;
using ControleFaturamentoJnx.Models;
using Microsoft.AspNetCore.Authorization;

namespace ControleFaturamentoJnx.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private readonly ControleFaturamentoContext _context;

        public LoginController(ControleFaturamentoContext context)
        {
            _context = context;
        }      

     

        // GET: Logins/Create
        public IActionResult Login()
        {
            return View();
        }
        
        
    }
}
