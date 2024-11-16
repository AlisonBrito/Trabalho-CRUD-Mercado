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

namespace TesteTrabalho1
{
    public partial class FormCRUDFuncionarios : Form
    {
        public FormCRUDFuncionarios()
        {
            InitializeComponent();
        }
        Thread tr;
        GerenciadorBD gbd = new GerenciadorBD();

        ///////////////// PARTE ONDE ESTA INSERINDO O QUE HÁ NA TABELA DO BANCO DE DADOS NO DATAGRIDVIEW /////////////////
        private void FormCRUDFuncionarios_Load(object sender, EventArgs e)
        {
            string connection = gbd.getConnectionString();

            MySqlConnection conn = new MySqlConnection(connection);

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select id_funcionarios, nome,idade,CPF,funcao,email,login,senha from funcionarios";

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable produtos = new DataTable();

                da.Fill(produtos);

                dataGridView1.DataSource = produtos;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        ///////////////// PARTE ONDE ESTA DELETANDO O QUE HÁ NA TABELA DO BANCO DE DADOS NO DATAGRIDVIEW /////////////////
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string connection = gbd.getConnectionString();

            MySqlConnection conn = new MySqlConnection(connection);

            if (dataGridView1.Columns[e.ColumnIndex] is
                DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                MessageBox.Show("Funcionando");
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "delete from funcionarios where id_funcionarios = @id";
                    cmd.Parameters.AddWithValue("@id",
                        dataGridView1.SelectedRows[0].Cells[1].Value);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "select id_funcionarios, nome,idade,CPF,funcao,email,login,senha from funcionarios";

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    DataTable produtos = new DataTable();

                    da.Fill(produtos);

                    dataGridView1.DataSource = produtos;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        ///////////////// PARTE ONDE ESTA ATUALIZANDO O QUE HÁ NA TABELA DO BANCO DE DADOS NO DATAGRIDVIEW /////////////////
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            string connection = gbd.getConnectionString();

            MySqlConnection conn = new MySqlConnection(connection);

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "update produtos set "
                    + dataGridView1.Columns[e.ColumnIndex].HeaderText +
                    " " +
                    "= @value where id_produtos = @id";

                cmd.Parameters.AddWithValue
                    ("@value", dataGridView1.SelectedRows[0].Cells[e.ColumnIndex].Value);
                cmd.Parameters.AddWithValue
                    ("@id", dataGridView1.SelectedRows[0].Cells[1].Value);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Funcionario alterado com sucesso");
            }
            catch (Exception)
            {

            }
        }


        ///////////////// PARTE ONDE ESTA ENCAMINHANDO O BOTÃO PARA OUTRA TELA /////////////////
        private void janelaCadastroFuncionarios(object obj)
        {
            Application.Run(new FormCadastroFuncionario());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            tr = new Thread(janelaCadastroFuncionarios);
            tr.SetApartmentState(ApartmentState.STA);
            tr.Start();
        }
    }
}
