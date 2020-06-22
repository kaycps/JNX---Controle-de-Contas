using ControleFaturamentoJnx.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFaturamentoJnx.Data
{
    public class ControleFaturamentoContext : DbContext
    {
      

        public ControleFaturamentoContext(DbContextOptions<ControleFaturamentoContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder mb)
        {           
                        
            mb.Entity<Fatura>()
                .HasOne<Cliente>(c => c.Cliente)
                .WithMany(f => f.Faturas)
                .HasForeignKey(f => f.ClienteID);

            mb.Entity<Parcela>()
                .HasOne<Fatura>(f => f.Fatura)
                .WithMany(f => f.Parcelas)
                .HasForeignKey(p => p.FaturaID);

            mb.Entity<Endereco>()
                .HasOne<Cliente>(e => e.Cliente)
                .WithOne(c => c.Endereco);

            mb.Entity<Despesa>()
                .HasOne<TipoCusto>(d => d.TipoCusto)
                .WithMany(d => d.Despesas)
                .HasForeignKey(d => d.TipoCustoID);

            mb.Entity<EnderecoFuncionario>()
                .HasOne<Funcionario>(f => f.Funcionario)
                .WithOne(e=>e.EnderecoFuncionario);
                

        }

        public DbSet<Fatura> Faturas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<ControleFaturamentoJnx.Models.Despesa> Despesa { get; set; }
        public DbSet<ControleFaturamentoJnx.Models.TipoCusto> TipoCusto { get; set; }
        public DbSet<ControleFaturamentoJnx.Models.Login> Login { get; set; }
        public DbSet<ControleFaturamentoJnx.Models.PrecoCusto> PrecoCusto { get; set; }
        public DbSet<ControleFaturamentoJnx.Models.VariaveisDeCalculo> VariaveisDeCalculo { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<EnderecoFuncionario> EnderecoFuncionarios { get; set; }
        public DbSet<ControleFaturamentoJnx.Models.Producao> Producao { get; set; }


    }
}
