using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;

namespace TesteTrabalho1.Classes
{
    internal class ValidadorDeTextos
    {
        public bool ValidadorSenha(string senha)
        {
            bool MAIUSCULA = false;
            bool MINISCULA = false;
            bool CARACTERESPECIAL = false;

            foreach (char c in senha)
            {
                if (c >= 65 && c <= 90)
                { 
                    MAIUSCULA = true;
                }

                if (c >= 97 && c <= 122)
                { 
                    MINISCULA = true;
                }

                if ((c >= 33 && c <= 47) || (c >= 58 && c <= 64) || (c >= 91 && c <= 96) || (c >= 123 && c <= 126))
                { 
                    CARACTERESPECIAL = true;
                }
               
                if (MAIUSCULA && MINISCULA && CARACTERESPECIAL)
                {
                    break;
                }
            } 
            if (!MAIUSCULA || !MINISCULA || !CARACTERESPECIAL)
                {
                    MessageBox.Show("A senha deve conter letras maiusculas, minusculas e caracteres especiais");
                }
            return MAIUSCULA && MINISCULA && CARACTERESPECIAL;
        }

        public bool ValidadorEmail(string email)
        {
            bool EMAIL = false;

            foreach (char z in email)
            {
                if (z == '@')
                {
                    EMAIL = true;
                    break;
                }
            }
            if (!EMAIL)
            {
                MessageBox.Show("O email deve conter um '@'");
            }
            return EMAIL;
        }

        public bool ValidadorNome(string nome)
        {
            bool NOME = false;
            if (nome.Length <= 3)
            {
                MessageBox.Show("O nome deve ter mais de 3 caracteres.");
            }
            else
            {
                NOME = true;
            }
            return NOME;
        }

        public bool ValidadorIdade(string ano)
        {
            bool IDADE = false;

            if (int.TryParse(ano, out int idade))
            {
                if (idade <= 12)
                {
                    MessageBox.Show("Você deve ter mais de 12 anos");
                }
                else
                {
                    IDADE = true;
                }
            }
            return IDADE;
        }

        public bool ValidadorCampoVazioFuncionario(string campo1, string campo2, string campo3, string campo4, string campo5, string campo6, string campo7)
        {
            bool CAMPO = false; 
            if (string.IsNullOrWhiteSpace(campo1) || string.IsNullOrWhiteSpace(campo2)
                || string.IsNullOrWhiteSpace(campo3) || string.IsNullOrWhiteSpace(campo4)
                || string.IsNullOrWhiteSpace(campo5) || string.IsNullOrWhiteSpace(campo6)
                || string.IsNullOrWhiteSpace(campo7))
            {
                MessageBox.Show("Preencha todas as informações");
                CAMPO = false; 
            }
            return CAMPO;
        }

        public bool ValidadorCampoVazioCliente(string campo1, string campo2, string campo3)
        {
            bool CAMPOS = false;
            if (string.IsNullOrWhiteSpace(campo1) || string.IsNullOrWhiteSpace(campo2) || string.IsNullOrWhiteSpace(campo3))
            {
                MessageBox.Show("Preencha todas as informações");
                CAMPOS = false;
            }
            return CAMPOS;
        }

        public bool ValidadorCampoVazioProdutos(string campo1, string campo2, string campo3, string campo4, string campo5)
        {
            bool CAMPOS = false;

            if (string.IsNullOrWhiteSpace(campo1) || string.IsNullOrWhiteSpace(campo2)
              || string.IsNullOrWhiteSpace(campo3) || string.IsNullOrWhiteSpace(campo4)
              || string.IsNullOrWhiteSpace(campo5))
            {
                MessageBox.Show("Preencha todas as informações");
                CAMPOS = false;
            }
            return CAMPOS;
        }

        public bool ValidadorQuantidadeProdutos(string quantidadeProdutos)
        {
            bool QUANTIDADE = false;
            foreach (char l in quantidadeProdutos)
            {
                if (l >= 48 && l <= 57)
                {
                    QUANTIDADE = true;
                }
            }
            if(!QUANTIDADE)
            {
                MessageBox.Show("Apenas é aceito números para a quantidade");
            }
            return QUANTIDADE;
        }

        public bool ValidadorQuantidadeNegativa(string quantidaNegativa)
        {
            bool QUANTIDADE = false;
            if (int.TryParse(quantidaNegativa, out int quantidade))
            {
                if (quantidade <= 0)
                {
                    MessageBox.Show("A quantidade de produtos deve ser maior que 0");
                }
                else
                {
                    QUANTIDADE = true;
                }
            }
            return QUANTIDADE;
        }

        public bool ValidadorValorNegativo(string valorNegativo)
        {
            bool VALOR = false;
            if (int.TryParse(valorNegativo, out int valor))
            {
                if (valor <= 0)
                {
                    MessageBox.Show("O valor do produto deve ser maior que 0");
                }
                else
                {
                    VALOR = true;
                }
            }
            return VALOR;
        }
    }
}
