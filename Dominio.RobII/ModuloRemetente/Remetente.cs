using Dominio.RobII.ModuloMensagem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.RobII.ModuloMembro
{
    public class Remetente
    {
        public Remetente()
        {
            
        }
        public Remetente(string nome)
        {
            nome = Nome;
        }
        public string Nome { get; set; }
    }
}
