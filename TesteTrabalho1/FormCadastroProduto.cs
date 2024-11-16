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
    public partial class FormCadastroProduto : Form
    {
        public FormCadastroProduto()
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
            bool QUANTIDADE = false;

            // VÊ SE TODOS ESPAÇOS FORAM PREENCHIDOS 
            if (validador.ValidadorCampoVazioProdutos(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text))
            {
                return;
            }

            // LÊ SE O NOME TEM MAIS DE 3 LETRAS 
            if (validador.ValidadorNome(textBox1.Text))
            {
                NOME = true;
            }

            // VALIDANDO SE SÓ FORAM ESCRITOS NÚMEROS NA QUANTIDADE
            if (validador.ValidadorQuantidadeProdutos(textBox4.Text))
            {
               QUANTIDADE = true;
            }

            if (NOME && QUANTIDADE)
            {
                MessageBox.Show("Cadastrado");

                MySqlCommand cmd = new MySqlCommand();
                string connection = gbd.getConnectionString();
                MySqlConnection conn = new MySqlConnection(connection);

                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO produtos (nome,NCM,unidade,quantidade,valor) VALUES " +
                    "(@nome, @NCM, @unidade,@quantidade,@valor)";
                    cmd.Parameters.AddWithValue("@nome", textBox1.Text);
                    cmd.Parameters.AddWithValue("@NCM", textBox2.Text);
                    cmd.Parameters.AddWithValue("@unidade", textBox3.Text);
                    cmd.Parameters.AddWithValue("@quantidade", textBox4.Text);
                    cmd.Parameters.AddWithValue("@valor", textBox5.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Produto cadastrado com sucesso");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                }
            }
        }


        ///////////////// PARTE ONDE ESTA ENCAMINHANDO OS BOTÕES PARA OUTRAS TELAS /////////////////
        private void janelaCRUDProduto(object obj)
        {
            Application.Run(new FormCRUDProduto());
        }

        private void janelaTelaIncial(object obj) 
        {
            Application.Run(new FormTelaInicial());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            tr = new Thread(janelaCRUDProduto);
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
