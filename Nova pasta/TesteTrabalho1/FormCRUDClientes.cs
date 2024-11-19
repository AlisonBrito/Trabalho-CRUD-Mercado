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
    public partial class FormCRUDClientes : Form
    {
        public FormCRUDClientes()
        {
            InitializeComponent();
        }
        Thread tr;
        GerenciadorBD gbd = new GerenciadorBD();

        ///////////////// PARTE ONDE ESTA INSERINDO O QUE HÁ NA TABELA DO BANCO DE DADOS NO DATAGRIDVIEW /////////////////
        private void FormCRUDClientes_Load(object sender, EventArgs e)
        {

            string connection = gbd.getConnectionString();

            MySqlConnection conn = new MySqlConnection(connection);

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT id_clientes, nome, CPF,dt_registro FROM clientes";

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable clientes = new DataTable();

                da.Fill(clientes);

                dataGridView1.DataSource = clientes;

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
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;

                  
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);

                    cmd.CommandText = "DELETE FROM clientes WHERE id_clientes = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                   
                    cmd.CommandText = "SELECT id_clientes, nome, CPF,dt_registro FROM clientes";
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable clientes = new DataTable();
                    da.Fill(clientes);
                    dataGridView1.DataSource = clientes;

                    MessageBox.Show("Cliente deletado com sucesso.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao deletar cliente: " + ex.Message);
                }
                finally
                {
                    conn.Close();
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
                cmd.CommandText = "update clientes set "
                    + dataGridView1.Columns[e.ColumnIndex].HeaderText +
                    " " +
                    "= @value where id_clientes = @id";

                cmd.Parameters.AddWithValue
                    ("@value", dataGridView1.SelectedRows[0].Cells[e.ColumnIndex].Value);
                cmd.Parameters.AddWithValue
                    ("@id", dataGridView1.SelectedRows[0].Cells[1].Value);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Cliente alterado com sucesso");
            }
            catch (Exception)
            {

            }
        }

        ///////////////// PARTE ONDE ESTA ENCAMINHANDO O BOTÃO PARA OUTRA TELA /////////////////
        private void janelaCadastroClientes(object obj)
        {
            Application.Run(new FormCadastroCliente());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            tr = new Thread(janelaCadastroClientes);
            tr.SetApartmentState(ApartmentState.STA);
            tr.Start();
        }
    }
}
