using System.Net.Sockets;
using System.Text.Json;
using System.Text;
using Dominio.RobII.ModuloMembro;

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

    public List<(int id, string nome)> ReceberSideBar()
    {
        List<(int id, string nome)> remetentes = new List<(int id, string nome)>();

        try
        {
            // 1. Ler JSON inicial
            byte[] bufferJson = new byte[4096];
            int bytesLidosJson = stream.Read(bufferJson, 0, bufferJson.Length);

            if (bytesLidosJson == 0)
            {
                Console.WriteLine("Nenhum dado recebido do servidor.");
                return remetentes;
            }

            string json = Encoding.UTF8.GetString(bufferJson, 0, bytesLidosJson);
            Console.WriteLine("JSON recebido: " + json);

            // 2. Desserializar o JSON em uma lista de objetos Remetente
            var listaRemetentes = JsonSerializer.Deserialize<List<Remetente>>(json);

            if (listaRemetentes != null)
            {
                // 3. Mapear para a lista de tuplas (id, nome)
                foreach (var remetente in listaRemetentes)
                {
                    remetentes.Add((remetente.Id, remetente.Nome));
                }
            }

            return remetentes;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao ler dados da sidebar: " + ex.Message);
            return remetentes;
        }
    }

}
