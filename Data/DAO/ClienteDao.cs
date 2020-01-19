using ControleFaturamentoJnx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFaturamentoJnx.Data.DAO
{
    public class ClienteDao
    {

        private ControleFaturamentoContext _context;
        public ClienteDao(ControleFaturamentoContext context)
        {
            _context = context;
        }
        

        public  void AdicionarFaturaCliente(Cliente cliente, Fatura fatura, Parcela parcela)
        {            

                List<Parcela> parcelas = AddParcelaFatura(fatura, parcela);

                fatura.Cliente = cliente;
                fatura.Parcelas = parcelas;

                _context.AddRange(fatura);
                _context.SaveChanges();
               
            
        }

        public List<Cliente> BuscarAllClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            foreach (var cliente in _context.Clientes)
            {
                clientes.Add(cliente);
            }

            return clientes;
        }

        private  List<Parcela> AddParcelaFatura(Fatura fatura, Parcela parcela)
        {
            int flag = 0;
            List<Parcela> parcelas = new List<Parcela>();
           
               while (flag < fatura.NumeroParcelas) {

                    DateTime vencimento = new DateTime().AddDays(parcela.Vencimento.Day).AddMonths(parcela.Vencimento.Month+flag)
                                                        .AddYears(parcela.Vencimento.Year);
                    parcelas.Add(new Parcela { Fatura=fatura, ValorParcela = (fatura.ValorFatura/fatura.NumeroParcelas),
                                                Vencimento = vencimento});

                    flag++;
               }

               return parcelas;
            
        }
    }
}
