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
    public class VariaveisDeCalculoController : Controller
    {
        private readonly ControleFaturamentoContext _context;

        public VariaveisDeCalculoController(ControleFaturamentoContext context)
        {
            _context = context;
        }

        // GET: VariaveisDeCalculoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.VariaveisDeCalculo.ToListAsync());
        }

        // GET: VariaveisDeCalculoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variaveisDeCalculo = await _context.VariaveisDeCalculo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (variaveisDeCalculo == null)
            {
                return NotFound();
            }

            return View(variaveisDeCalculo);
        }

        // GET: VariaveisDeCalculoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VariaveisDeCalculoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Pis,Confins,Fgts,Inss,ComissaoVendedor,Icms,FreteVenda,FreteCompra,ProducaoMensal")] VariaveisDeCalculo variaveisDeCalculo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(variaveisDeCalculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(variaveisDeCalculo);
        }

        // GET: VariaveisDeCalculoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variaveisDeCalculo = await _context.VariaveisDeCalculo.FindAsync(id);
            if (variaveisDeCalculo == null)
            {
                return NotFound();
            }
            return View(variaveisDeCalculo);
        }

        // POST: VariaveisDeCalculoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Pis,Confins,Fgts,Inss,ComissaoVendedor,Icms,FreteVenda,FreteCompra,ProducaoMensal")] VariaveisDeCalculo variaveisDeCalculo)
        {
            if (id != variaveisDeCalculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(variaveisDeCalculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VariaveisDeCalculoExists(variaveisDeCalculo.Id))
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
            return View(variaveisDeCalculo);
        }

        // GET: VariaveisDeCalculoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variaveisDeCalculo = await _context.VariaveisDeCalculo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (variaveisDeCalculo == null)
            {
                return NotFound();
            }

            return View(variaveisDeCalculo);
        }

        // POST: VariaveisDeCalculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var variaveisDeCalculo = await _context.VariaveisDeCalculo.FindAsync(id);
            _context.VariaveisDeCalculo.Remove(variaveisDeCalculo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VariaveisDeCalculoExists(int id)
        {
            return _context.VariaveisDeCalculo.Any(e => e.Id == id);
        }
    }
}
