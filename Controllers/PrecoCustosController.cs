using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControleFaturamentoJnx.Data;
using ControleFaturamentoJnx.Models;



namespace ControleFaturamentoJnx.Controllers
{
    public class PrecoCustosController : Controller
    {
        private  ControleFaturamentoContext _context;
        
        public PrecoCustosController(ControleFaturamentoContext context)
        {
            _context = context;
           
        }

        // GET: PrecoCustos
        public async Task<IActionResult> Index()
        {            
            var CustoKg = _context.PrecoCusto.Where(p=>p.Peso==1000);
            int producao =  _context.VariaveisDeCalculo.Select(v => v.ProducaoMensal).Sum();
            var variaveis = _context.VariaveisDeCalculo;
                      
            var trigoKg = CustoKg.Select(p => p.Trigo).First() / 25;
            var vitaminaKg = (CustoKg.Select(p => p.Vitamina).First()) / 1800;
            var licetinaKg = (CustoKg.Select(p => p.Lecitina).First() / 20)*0.6 / 1800;
            var embalagemKg = CustoKg.Select(p => p.Embalagem).First();
            var fita = CustoKg.Select(p => p.Fita).First() / 5400;
            var bandejaKg = CustoKg.Select(p => p.Bandeja).First();
            var lenha = CustoKg.Select(p => p.Lenha).First() / 3680;
            var energia = CustoKg.Select(p => p.Energia).First() / producao;
            var caixaKg = CustoKg.Select(p => p.Caixa).First() / 10;
            var custoFixo = CustoKg.Select(p => p.CustoFixo).First() / producao;
            var maoObra = _context.Funcionarios.Sum(f=>f.Salario) / producao;

            //Preco de custo KG
            double precoCustoKg = maoObra + caixaKg + energia + lenha + fita + embalagemKg + licetinaKg + vitaminaKg + trigoKg+ custoFixo;
            double precoCustoLucroKg = precoCustoKg * (1 + (CustoKg.Select(v => v.PorcentagemLucro).FirstOrDefault()/100));
            double custoTotalKg = precoCustoLucroKg * (1 + (CustoKg.Select(v => v.PorcentagemVendedor).FirstOrDefault() / 100));
            double precoTotalKgFrete = custoTotalKg * (1 + variaveis.Select(v => v.FreteVenda).First() / 100);

            //Preco de CUsto 400G
            double precoCusto400 = precoCustoKg * 0.4;
            double precoCustoLucro400 = precoCusto400 * (1+CustoKg.Select(p => p.PorcentagemLucro).First() / 100);
            double precoTotal400 = precoCustoLucro400 * (1+CustoKg.Select(p => p.PorcentagemVendedor).First() / 100);
            double precoTotal400Frete = precoTotal400 * (1 + variaveis.Select(v => v.FreteVenda).First() / 100);


            ViewBag.precoCustoKg = precoCustoKg.ToString("C");            
            ViewBag.precoCustoLucroKg = precoCustoLucroKg.ToString("C");
            ViewBag.custoTotalKg = custoTotalKg.ToString("C");
            ViewBag.precoTotalKgFrete = precoTotalKgFrete.ToString("C");

            ViewBag.precoCusto400 = precoCusto400.ToString("C");
            ViewBag.precoCustoLucro400 = precoCustoLucro400.ToString("C");
            ViewBag.precoTotal400 = precoTotal400.ToString("C");
            ViewBag.precoTotal400Frete = precoTotal400Frete.ToString("C");

            return View(await _context.PrecoCusto.ToListAsync());
        }

        // GET: PrecoCustos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var precoCusto = await _context.PrecoCusto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (precoCusto == null)
            {
                return NotFound();
            }

            return View(precoCusto);
        }

        // GET: PrecoCustos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PrecoCustos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Peso,Trigo,Embalagem,Caixa,Folha,Vitamina,Lecitina,Fita,Bandeja,Energia,Lenha,Manutenção,PorcentagemVendedor,PorcentagemLucro,CustoFixo")] PrecoCusto precoCusto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(precoCusto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(precoCusto);
        }

        // GET: PrecoCustos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var precoCusto = await _context.PrecoCusto.FindAsync(id);
            if (precoCusto == null)
            {
                return NotFound();
            }
            return View(precoCusto);
        }

        // POST: PrecoCustos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Peso,Trigo,Embalagem,Caixa,Folha,Vitamina,Lecitina,Fita,Bandeja,Energia,Lenha,Manutenção,PorcentagemVendedor,PorcentagemLucro,CustoFixo")] PrecoCusto precoCusto)
        {
            if (id != precoCusto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(precoCusto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrecoCustoExists(precoCusto.Id))
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
            return View(precoCusto);
        }

        // GET: PrecoCustos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var precoCusto = await _context.PrecoCusto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (precoCusto == null)
            {
                return NotFound();
            }

            return View(precoCusto);
        }

        // POST: PrecoCustos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var precoCusto = await _context.PrecoCusto.FindAsync(id);
            _context.PrecoCusto.Remove(precoCusto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrecoCustoExists(int id)
        {
            return _context.PrecoCusto.Any(e => e.Id == id);
        }
    }
}
