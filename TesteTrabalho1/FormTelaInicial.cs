/* 
 EXPLICAÇÃO DO PROJETO
 Código de CRUD de clientes, funcionarios e produtos. 
 Tem como objetivo C - Create(criar) (cadastrar clientes, funcionarios e produtos), onde foram feitas telas com esses objetivos.
 Nome das telas: FormCadastroCliente, FormCadastroFuncionario e FormCadastroProduto.

 Tem como objetivo R - Read(ler) (exibir clientes, funcionarios e produtos), onde foram feitas telas com esses objetivos.  
 Tem como objetivo U - Update(atualizar) (atualizar/editar clientes, funcionarios e produtos), onde foram feitas telas com esses objetivos. 
 Tem como objetivo D - Delete(deletar) (deletar clientes, funcionarios e produtos), onde foram feitas telas com esses objetivos. 
 Nome das telas: FormCRUDClientes, FormCRUDFuncionarios e FormCRUDProduto

 Este projeto foi feito para um trabalho na materia de Programação de Aplicativos do Curso de Desenvolvimento de Sistemas do Senai. 
 Onde foram usados os conhecimentos de conexão com o banco de dados, classes, instaciações das classes, validações, mudanças de uma tela para outra e orientação a objeto.
 Versão 1.2
 Nome: Alison Brito 
*/

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
    public partial class FormTelaInicial : Form
    {
        public FormTelaInicial()
        {
            InitializeComponent();
        }
        Thread tr;

        private void janelaCadastroCliente(object obj)
        {
            Application.Run(new FormCadastroCliente());
        }

        private void janelaCadastroFuncionario(object obj)
        {
            Application.Run(new FormCadastroFuncionario());
        }

        private void janelaCadastroProduto(object obj)
        {
            Application.Run(new FormCadastroProduto());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            tr = new Thread(janelaCadastroCliente);
            tr.SetApartmentState(ApartmentState.STA);
            tr.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            tr = new Thread(janelaCadastroFuncionario);
            tr.SetApartmentState(ApartmentState.STA);
            tr.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            tr = new Thread(janelaCadastroProduto);
            tr.SetApartmentState(ApartmentState.STA);
            tr.Start();
        }
    }
}
