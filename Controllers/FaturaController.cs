using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleFaturamentoJnx.Data;
using ControleFaturamentoJnx.Models;
using ControleFaturamentoJnx.Views.Shared.Enum;
using Microsoft.AspNetCore.Authorization;

namespace ControleFaturamentoJnx.Views
{
    [Authorize]
    public class FaturaController : Controller
    {
        private readonly ControleFaturamentoContext _context;

        public FaturaController(ControleFaturamentoContext context)
        {
            _context = context;

        }

        // GET: Fatura
        public async Task<IActionResult> Index()
        {
            var controleFaturamentoContext = _context.Faturas.Include(f => f.Cliente).AsNoTracking().OrderBy(f=>f.DataEmissao);
            return View(await controleFaturamentoContext.ToListAsync());
        }

        // GET: Fatura/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fatura = await _context.Faturas
                .Include(f => f.Cliente)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (fatura == null)
            {
                return NotFound();
            }

            return View(fatura);
        }

        // GET: Fatura/Create
        public IActionResult Create()
        {
            ViewData["ClienteID"] =  new SelectList( _context.Clientes, "ID", "Nome");
            
            
            return View();
        }

        // POST: Fatura/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DataEmissao,Tipo,Numero,NumeroParcelas,ValorFatura,Observacao,ClienteID")] Fatura fatura)
        {
            if (ModelState.IsValid)
            {
                int flag = 0;
                

                while (flag < fatura.NumeroParcelas)
                {

                    Parcela parcela = new Parcela()
                    {
                        Fatura = fatura,
                        ValorParcela = Math.Round(fatura.ValorFatura / fatura.NumeroParcelas,2),
                        Vencimento = new DateTime(fatura.DataEmissao.Year,
                                                   fatura.DataEmissao.Month+(flag+1),
                                                     fatura.DataEmissao.Day),
                                                    

                        Status = StatusEnum.Pagamento.Pendente.ToString()                        
                        
                    };

                    _context.Add(parcela);
                    flag++;
                };

                _context.Add(fatura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ID", "ID", fatura.ClienteID);
            return View(fatura);
        }

        // GET: Fatura/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fatura = await _context.Faturas.FindAsync(id);
            if (fatura == null)
            {
                return NotFound();
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ID", "Nome", fatura.ClienteID);
            return View(fatura);
        }

        // POST: Fatura/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DataEmissao,Tipo,Numero,NumeroParcelas,ValorFatura,Observacao,ClienteID")] Fatura fatura)
        {
            if (id != fatura.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fatura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaturaExists(fatura.ID))
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
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ID", "ID", fatura.ClienteID);
            return View(fatura);
        }

        // GET: Fatura/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fatura = await _context.Faturas
                .Include(f => f.Cliente)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (fatura == null)
            {
                return NotFound();
            }

            return View(fatura);
        }

        // POST: Fatura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fatura = await _context.Faturas.FindAsync(id);
            _context.Faturas.Remove(fatura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaturaExists(int id)
        {
            return _context.Faturas.Any(e => e.ID == id);
        }
    }
}
