
using Dominio.RobII.ModuloMembro;
using Dominio.RobII.ModuloMensagem;
using System;
using System.IO;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
namespace RobII
{
    public partial class TelaInicial : Form
    {
        private TcpClient clienteConexao;
        private NetworkStream streamConexao;

        string[,] listaDeMembros = new string[5, 9];
        List<(string id, string nome)> remetentes = new List<(string id, string nome)>();

        public Remetente membro = new Remetente();
        public readonly TelaInicial tela;
        public ControladorTelaInicial clientePersonalizado;

        public TelaInicial(ControladorTelaInicial cliente)
        {
            InitializeComponent();
            this.clientePersonalizado = cliente;
        }


        #region Registro no Servidor


        #endregion

        #region Eventos Registro
        private void buttonRegistrar_Click(object sender, EventArgs e)
        {

            if (!clientePersonalizado.Conectar())
            {
                MessageBox.Show("Não foi possível conectar ao servidor.");
                return;
            }

            string nomeUsuario = textBoxNomeUsuario.Text;

            membro.Nome = nomeUsuario;
            membro.DataHora = DateTime.Now;
            membro.Status = true;

            MandarIP();

            clientePersonalizado.EnviarReceberMensagem(membro);

            AtualizarTela();
            AtualizarSideBar();
        }

        private void AtualizarSideBar()
        {
            remetentes = clientePersonalizado.ReceberSideBar();

           // Console.WriteLine(id, nome);
        }

        private void MandarIP()
        {
            string ipLocal = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString();
            membro.Ip = ipLocal; //Arrumar 
            
        }

        private void AtualizarTela()
        {
            labelAlerta.Visible = false;
            //textBoxNomeUsuario.Visible = true;
            //buttonRegistrar.Visible = false;
            labelUsuario.Visible = true;
            labelUsuario.Text = membro.Nome;
            labelLinhaNome.Visible = false;
            panelMensagem.Visible = true;
            textBoxChat.Visible = true;
            pictureBoxMensagem1.Visible = true;
            pictureBoxMensagem2.Visible = true;
            pictureBoxMensagem3.Visible = true;
            pictureBoxMensagem4.Visible = true;
            textBoxNomeUsuario.Visible = false;

            labelNome.Visible = false;
            labelNomeUsuario.Text = membro.Nome;
            labelNomeUsuario.ForeColor = ColorTranslator.FromHtml("#ffe5e6");
            //pictureBoxUsuario.

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
