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
    public class ParcelaController : Controller
    {
        private readonly ControleFaturamentoContext _context;

        public ParcelaController(ControleFaturamentoContext context)
        {
            _context = context;
        }

        // GET: Parcela
        public async Task<IActionResult> Index()
        {
            var controleFaturamentoContext =  _context.Parcelas.Include(p => p.Fatura).AsNoTracking()
                                                                    .OrderBy(p=>p.Vencimento.Date.Day)
                                                                    .OrderBy(p=>p.Vencimento.Date.Month)
                                                                    .OrderBy(p=>p.Vencimento.Date.Year)
                                                                    ;


            List<Parcela> parcelasVencidas = _context.Parcelas.ToList();


            foreach (Parcela p in parcelasVencidas)
            {
                if (p.Vencimento<DateTime.Now && 
                    p.Status.Equals( StatusEnum.Pagamento.Pendente.ToString()))
                {
                    p.Status = StatusEnum.Pagamento.Vencido.ToString();
                    _context.Update(p);
                    await _context.SaveChangesAsync();
                }
                
            }

            //_context.SaveChangesAsync();
            return View(await controleFaturamentoContext.ToListAsync());
        }

        // GET: Parcela/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcela = await _context.Parcelas
                .Include(p => p.Fatura)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parcela == null)
            {
                return NotFound();
            }

            return View(parcela);
        }

        // GET: Parcela/Create
        public IActionResult Create()
        {
            ViewData["FaturaID"] = new SelectList(_context.Faturas, "ID", "Numero");
            return View();
        }

        // POST: Parcela/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ValorParcela,Vencimento,Status,FaturaID")] Parcela parcela)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parcela);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FaturaID"] = new SelectList(_context.Faturas, "ID", "ID", parcela.FaturaID);
            return View(parcela);
        }

        // GET: Parcela/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcela = await _context.Parcelas.FindAsync(id);
            if (parcela == null)
            {
                return NotFound();
            }
            ViewData["FaturaID"] = new SelectList(_context.Faturas, "ID", "Numero", parcela.FaturaID);
            return View(parcela);
        }

        // POST: Parcela/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ValorParcela,Vencimento,Status,FaturaID")] Parcela parcela)
        {
            if (id != parcela.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parcela);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParcelaExists(parcela.ID))
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
            ViewData["FaturaID"] = new SelectList(_context.Faturas, "ID", "ID", parcela.FaturaID);
            return View(parcela);
        }

        // GET: Parcela/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcela = await _context.Parcelas
                .Include(p => p.Fatura)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parcela == null)
            {
                return NotFound();
            }

            return View(parcela);
        }

        // POST: Parcela/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parcela = await _context.Parcelas.FindAsync(id);
            _context.Parcelas.Remove(parcela);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async void  ChangeStatus( Parcela parcela)
        {     
           

            if (ModelState.IsValid )
            {
                
                    _context.Update(parcela);
                   await _context.SaveChangesAsync();                              
                
            }
           
            
        }
        private bool ParcelaExists(int id)
        {
            return _context.Parcelas.Any(e => e.ID == id);
        }
    }
}
