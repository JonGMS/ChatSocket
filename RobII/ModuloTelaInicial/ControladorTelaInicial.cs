using System.Net.Sockets;
using System.Text.Json;
using System.Text;

public class ControladorTelaInicial
{
    public string ip { get; }
    public int porta { get; }

    private TcpClient cliente;
    private NetworkStream stream;

    public ControladorTelaInicial(string ip, int porta)
    {
        this.ip = ip;
        this.porta = porta;
    }

    public bool Conectar()
    {
        try
        {
            cliente = new TcpClient(ip, porta);
            stream = cliente.GetStream();
            return true;
        }
        catch (SocketException ex)
        {
            Console.WriteLine("Erro ao conectar: " + ex.Message);
            return false;
        }
    }

    public void FecharConexao()
    {
        stream?.Close();
        cliente?.Close();
    }

    public string EnviarReceberMensagem<T>(T objetoMensagem)
    {
        try
        {
            string json = JsonSerializer.Serialize(objetoMensagem);
            byte[] dados = Encoding.UTF8.GetBytes(json);

            stream.Write(dados, 0, dados.Length);
            Console.WriteLine("Mensagem enviada ao servidor: " + json);

            byte[] buffer = new byte[4096];
            int bytesLidos = stream.Read(buffer, 0, buffer.Length);
            string resposta = Encoding.UTF8.GetString(buffer, 0, bytesLidos);

            Console.WriteLine("Resposta recebida: " + resposta);
            return resposta;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao enviar/receber: " + ex.Message);
            return string.Empty;
        }
    }

    public List<(string id, string nome)> ReceberSideBar()
    {
        List<(string id, string nome)> remetentes = new List<(string id, string nome)>();

        try
        {
            // 1. Ler JSON inicial
            byte[] bufferJson = new byte[4096];
            int bytesLidosJson = stream.Read(bufferJson, 0, bufferJson.Length);
            string json = Encoding.UTF8.GetString(bufferJson, 0, bytesLidosJson);
            Console.WriteLine("JSON recebido: " + json);

            

            
            return remetentes;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao ler dados da sidebar: " + ex.Message);
            return remetentes;
        }
    }

}
