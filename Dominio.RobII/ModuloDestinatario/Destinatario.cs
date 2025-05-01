using Dominio.RobII.ModuloMensagem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.RobII.ModuloDestinatario
{
    public class Destinatario
    {
        public Destinatario()
        {
            
        }
        public Destinatario(int id, string nome, string ip, Mensagem mensagem)
        {
            id = Id;
            nome = Nome;
            ip = Ip;
            mensagem = Mensagem;
        }
        
        public int Id { get; set; } 
        public string Nome { get; set; }
        public string Ip { get; set; }
        public Mensagem Mensagem { get; set; }
    }
}
