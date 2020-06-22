using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFaturamentoJnx.Models
{
    public class Funcionario
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataAdimissao { get; set; }
        public double Salario { get; set; }
        public bool Ativo { get; set; }
        public EnderecoFuncionario EnderecoFuncionario { get; set; }

    }
}
