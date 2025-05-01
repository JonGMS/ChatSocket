using Dominio.RobII.ModuloMembro;
using Dominio.RobII.ModuloMensagem;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
namespace RobII
{
    public partial class TelaInicial : Form
    {
        public Remetente membro = new Remetente();
    
        public TelaInicial(TcpClient cliente)
        {
            InitializeComponent();
        }


        #region Registro no Servidor
        private void ConectarServidor(string dados)
        {


        }

        #endregion

        #region Eventos Registro
        private void buttonRegistrar_Click(object sender, EventArgs e)
        {
            membro.Nome = textBoxNomeUsuario.Text;

            TcpClient cliente = new TcpClient("127.0.0.1", 20000);
            NetworkStream stream = cliente.GetStream();

            string json = JsonSerializer.Serialize(membro);
            byte[] dados = Encoding.UTF8.GetBytes(json);
            stream.Write(dados, 0, dados.Length);

            AtualizarTela();
        }

        private void AtualizarTela()
        {
            labelAlerta.Visible = false;

        }

        #endregion

        #region Eventos Mensagem
        private void buttonArquivo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "C:\\", // Diretório inicial
                Title = "Selecione um arquivo",
                Filter = "Todos os arquivos (.)|.|Imagens (.jpg;.png)|.jpg;.png",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            // Exibe a janela e verifica se o usuário selecionou um arquivo
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                Console.WriteLine("Arquivo selecionado: " + filePath);
            }
            else
            {
                Console.WriteLine("Nenhum arquivo selecionado.");
            }
        }
        #endregion





    }
}
