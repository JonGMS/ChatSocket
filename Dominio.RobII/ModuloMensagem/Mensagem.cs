namespace Dominio.RobII.ModuloMensagem
{
    public class Mensagem
    {
        public Mensagem()
        {
            
        }
        public Mensagem(string ip, int id, string mensagemTexto, DateTime data)
        {
            Ip = ip;
            IdMembro = id;
            
            MensagemTexto = mensagemTexto;
            DataHora = data;
        }

        public string Ip { get; set; }
        public int IdMembro { get; set; }
        public string MensagemTexto { get; set; }
        public DateTime DataHora { get; set; }
    }
}
