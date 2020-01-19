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
    public class TipoCustoesController : Controller
    {
        private readonly ControleFaturamentoContext _context;

        public TipoCustoesController(ControleFaturamentoContext context)
        {
            _context = context;
        }

        // GET: TipoCustoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoCusto.ToListAsync());
        }

        // GET: TipoCustoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCusto = await _context.TipoCusto
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tipoCusto == null)
            {
                return NotFound();
            }

            return View(tipoCusto);
        }

        // GET: TipoCustoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoCustoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome")] TipoCusto tipoCusto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoCusto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoCusto);
        }

        // GET: TipoCustoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCusto = await _context.TipoCusto.FindAsync(id);
            if (tipoCusto == null)
            {
                return NotFound();
            }
            return View(tipoCusto);
        }

        // POST: TipoCustoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome")] TipoCusto tipoCusto)
        {
            if (id != tipoCusto.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoCusto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoCustoExists(tipoCusto.ID))
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
            return View(tipoCusto);
        }

        // GET: TipoCustoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCusto = await _context.TipoCusto
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tipoCusto == null)
            {
                return NotFound();
            }

            return View(tipoCusto);
        }

        // POST: TipoCustoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoCusto = await _context.TipoCusto.FindAsync(id);
            _context.TipoCusto.Remove(tipoCusto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoCustoExists(int id)
        {
            return _context.TipoCusto.Any(e => e.ID == id);
        }
    }
}
