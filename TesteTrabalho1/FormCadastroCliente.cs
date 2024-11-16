using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TesteTrabalho1.Classes;

namespace TesteTrabalho1
{
    public partial class FormCadastroCliente : Form
    {
        public FormCadastroCliente()
        {
            InitializeComponent();
        }
        Thread tr;
        GerenciadorBD gbd = new GerenciadorBD();
        ValidadorDeTextos validador = new ValidadorDeTextos();


        ///////////////// PARTE ONDE ESTA FAZENDO VALIDAÇÕES /////////////////
        private void button1_Click(object sender, EventArgs e)
        {

            bool NOME = false;

            if (validador.ValidadorCampoVazioCliente(textBox1.Text, maskedTextBox1.Text, maskedTextBox2.Text))
            {
                return;
            }

            if (validador.ValidadorNome(textBox1.Text))
            {
                NOME = true;
            }

            if (NOME)
            {
                MessageBox.Show("Cadastrado");

                MySqlCommand cmd = new MySqlCommand();
                string connection = gbd.getConnectionString();
                MySqlConnection conn = new MySqlConnection(connection);

                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO clientes (nome,CPF,dt_registro) " +
                        "VALUES (@nome, @CPF, @dt_registro)";
                    cmd.Parameters.AddWithValue("@nome", textBox1.Text);
                    cmd.Parameters.AddWithValue("@CPF", maskedTextBox1.Text);
                    cmd.Parameters.AddWithValue("@dt_registro", maskedTextBox2.Text);


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
                    textBox1.Clear();
                    maskedTextBox1.Clear();
                    maskedTextBox2.Clear();
                }
            }
        }



        ///////////////// PARTE ONDE ESTA ENCAMINHANDO OS BOTÕES PARA OUTRAS TELAS /////////////////
        private void janelaCRUDClientes(object obj)
        {
            Application.Run(new FormCRUDClientes());
        }

        private void janelaTelaIncial(object obj)
        {
            Application.Run(new FormTelaInicial());
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            tr = new Thread(janelaCRUDClientes);
            tr.SetApartmentState(ApartmentState.STA);
            tr.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            tr = new Thread(janelaTelaIncial);
            tr.SetApartmentState(ApartmentState.STA);
            tr.Start();
        }
    }
    
}
