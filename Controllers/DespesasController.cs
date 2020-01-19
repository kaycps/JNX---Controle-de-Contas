using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleFaturamentoJnx.Data;
using ControleFaturamentoJnx.Models;

namespace ControleFaturamentoJnx.Views
{
    public class DespesasController : Controller
    {
        private readonly ControleFaturamentoContext _context;

        public DespesasController(ControleFaturamentoContext context)
        {
            _context = context;
        }

        // GET: Despesas
        public async Task<IActionResult> Index()
        {
            var controleFaturamentoContext = _context.Despesa.Include(d => d.TipoCusto);
            return View(await controleFaturamentoContext.ToListAsync());
        }

        // GET: Despesas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var despesa = await _context.Despesa
                .Include(d => d.TipoCusto)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (despesa == null)
            {
                return NotFound();
            }

            return View(despesa);
        }

        // GET: Despesas/Create
        public IActionResult Create()
        {
            
            ViewData["TipoCustoID"] = new SelectList(_context.Set<TipoCusto>(), "ID", "Nome");
            return View();
        }

        // POST: Despesas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TipoCustoID,Valor,Data,Observacao")] Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(despesa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoCustoID"] = new SelectList(_context.Set<TipoCusto>(), "ID", "ID", despesa.TipoCustoID);
            return View(despesa);
        }

        // GET: Despesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var despesa = await _context.Despesa.FindAsync(id);
            if (despesa == null)
            {
                return NotFound();
            }
            ViewData["TipoCustoID"] = new SelectList(_context.Set<TipoCusto>(), "ID", "Nome", despesa.TipoCustoID);
            return View(despesa);
        }

        // POST: Despesas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TipoCustoID,Valor,Data,Observacao")] Despesa despesa)
        {
            if (id != despesa.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(despesa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DespesaExists(despesa.ID))
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
            ViewData["TipoCustoID"] = new SelectList(_context.Set<TipoCusto>(), "ID", "ID", despesa.TipoCustoID);
            return View(despesa);
        }

        // GET: Despesas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var despesa = await _context.Despesa
                .Include(d => d.TipoCusto)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (despesa == null)
            {
                return NotFound();
            }

            return View(despesa);
        }

        // POST: Despesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var despesa = await _context.Despesa.FindAsync(id);
            _context.Despesa.Remove(despesa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DespesaExists(int id)
        {
            return _context.Despesa.Any(e => e.ID == id);
        }
    }
}
