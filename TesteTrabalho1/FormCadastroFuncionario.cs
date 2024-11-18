using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TesteTrabalho1.Classes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;

namespace TesteTrabalho1
{
    public partial class FormCadastroFuncionario : Form
    {
        public FormCadastroFuncionario()
        {
            InitializeComponent();
        }
        Thread tr;
        private const int WorkFactor = 12;
        GerenciadorBD gbd = new GerenciadorBD();
        ValidadorDeTextos validador = new ValidadorDeTextos();

        ///////////////// PARTE ONDE ESTA FAZENDO VALIDAÇÕES /////////////////
        private void button1_Click(object sender, EventArgs e)
        {
            bool MAIUSCULA = false;
            bool MINISCULA = false;
            bool CARACTERESPECIAL = false;
            bool NOME = false;
            bool NUMEROIDADE = false;
            bool EMAIL = false;

            // VALIDANDO SE TODOS ESPAÇOS ESTÃO PREENCHIDOS, CÓDIGO ABAIXO
            if (validador.ValidadorCampoVazioFuncionario(maskedTextBox1.Text, textBox2.Text, maskedTextBox3.Text,
            maskedTextBox4.Text, maskedTextBox5.Text, maskedTextBox6.Text, textBox1.Text))
            {
                return;
            }

            // VALIDANDO SE ESCREVEU NUMERO PARA IDADE 
            if(validador.ValidadorNumeroIdade(textBox2.Text))
            {
                NUMEROIDADE = true;
            }

            // VALIDANDO O MÍNIMO DE IDADE PARA ENTRAR NO SISTEMA, CÓDIGO ABAIXO
            if (!validador.ValidadorIdade(textBox2.Text))
            {
                return; 
            }

            // VALIDANDO SE HÁ '@' NO EMAIL, CÓDIGO ABAIXO
            if (validador.ValidadorEmail(maskedTextBox5.Text))
            {
               EMAIL = true;
            }

            // VALIDANDO O TAMANHO DO NOME, CÓDIGO ABAIXO
            if (validador.ValidadorNome(maskedTextBox1.Text))
            {
                NOME = true;
            }
            
            // VALIDANDO A SENHA, CÓDIGO ABAIXO
            if (validador.ValidadorSenha(textBox1.Text))
            {
                MAIUSCULA = true;
                MINISCULA = true;
                CARACTERESPECIAL = true;
            }

            // CRIPTOGRAFANDO A SENHA 
            string senhaHash = HashPassword(textBox1.Text, WorkFactor);

            if (MAIUSCULA && MINISCULA && CARACTERESPECIAL && NOME && EMAIL && NUMEROIDADE)
            {
                MessageBox.Show("Cadastrado");

                MySqlCommand cmd = new MySqlCommand();
                string connection = gbd.getConnectionString();
                MySqlConnection conn = new MySqlConnection(connection);

                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO funcionarios (nome,idade,CPF,funcao,email,login,senha ) " +
                        "VALUES (@nome, @idade, @CPF, @funcao, @email, @login, @senha)";
                    cmd.Parameters.AddWithValue("@nome", maskedTextBox1.Text);
                    cmd.Parameters.AddWithValue("@idade", textBox2.Text);
                    cmd.Parameters.AddWithValue("@CPF", maskedTextBox3.Text);
                    cmd.Parameters.AddWithValue("@funcao", maskedTextBox4.Text);
                    cmd.Parameters.AddWithValue("@email", maskedTextBox5.Text);
                    cmd.Parameters.AddWithValue("@login", maskedTextBox6.Text);
                    cmd.Parameters.AddWithValue("@senha", senhaHash);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sucesso");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                    maskedTextBox1.Clear();
                    textBox2.Clear();
                    maskedTextBox3.Clear();
                    maskedTextBox4.Clear();
                    maskedTextBox5.Clear();
                    textBox1.Clear();
                }
            }
        }




        ///////////////// PARTE ONDE ESTA ENCAMINHANDO OS BOTÕES PARA OUTRAS TELAS /////////////////
        private void janelaCRUDFuncionarios(object obj)
        {
            Application.Run(new FormCRUDFuncionarios());
        }

        private void janelaTelaInicial(object obj)
        {
            Application.Run(new FormTelaInicial());
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            tr = new Thread(janelaCRUDFuncionarios);
            tr.SetApartmentState(ApartmentState.STA);
            tr.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            tr = new Thread(janelaTelaInicial);
            tr.SetApartmentState(ApartmentState.STA);
            tr.Start();
        }
    }
}



