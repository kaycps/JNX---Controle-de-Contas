using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleFaturamentoJnx.Data;
using ControleFaturamentoJnx.Models;

namespace ControleFaturamentoJnx.Controllers
{
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

        // POST: Logins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("ID,Usuario,Senha")] Login login)
        {
            if (ModelState.IsValid)
            {
                Login Consulta = _context.Login. Where(l => l.Usuario.Equals(login.Usuario.ToString())
                                         && l.Senha.Equals(login.Senha.ToString())).SingleOrDefault();

                if (login.Usuario.ToString()==Consulta.Usuario.ToString())
                {
                    if (login.Senha==Consulta.Senha)
                    {
                        return login.Senha.ToString() != Consulta.Senha.ToString() 
                                                      ? (IActionResult)View(login)
                                                      : Redirect("/Home/Index");

                    }

                    ModelState.AddModelError("", "Senha incorreta!");                    

                }

                ModelState.AddModelError("", "Usuario Invalido!");               

            }
            return View(login);
        }

        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            return View(login);
        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Usuario,Senha")] Login login)
        {
            if (id != login.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(login);
        }
      


        private bool LoginExists(int id)
        {
            return _context.Login.Any(e => e.ID == id);
        }
    }
}
