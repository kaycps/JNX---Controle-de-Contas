using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFaturamentoJnx.Models
{
    public class Endereco
    {   
      
        
        public int ID { get; set; } 
        
        public string Estado { get; set; }
        
        public string Cidade { get; set; }
       
        public string Bairro { get; set; }
        
        public string Rua { get; set; }
        
        public string Numero { get; set; }
        public string Cep { get; set; }        
        
        public int ClienteID { get; set; }
        public  Cliente Cliente { get; set; }

    }
}
