using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteTrabalho1
{
    internal class GerenciadorBD
    {
        private string connectionString = "server=localhost;port=3306;database=" +
            "teste;UID=root;password=''" +
            "";

        public string getConnectionString()
        {
            return this.connectionString;
        }
    }
}
