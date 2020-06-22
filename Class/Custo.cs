using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFaturamentoJnx.Models;

namespace ControleFaturamentoJnx.Class
{
    public  class Custo
    {
        
        public Custo()
        {

        }

        public static  double CalcTrigo(double ValorTrigo)
        {            
            return ValorTrigo/25;
        }
        public static double CalcVitamina(double ValorVitamina)
        {
            return ValorVitamina / 1800;
        }
        public static double CalcLicetina(double ValorLicetina)
        {
            return (ValorLicetina / 20) * 0.6 / 1800;
        }
        public static double CalcFita(double ValorFita)
        {
            return ValorFita / 5400;
        }
        public static double CalcLenha(double ValorLenha)
        {
            return ValorLenha / 3680;
        }

    }
}
