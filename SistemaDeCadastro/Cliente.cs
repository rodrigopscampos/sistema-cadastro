using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeCadastro
{
    public class Cliente
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime DtNascimento { get; private set; }

        public Cliente(int id, string nome, DateTime dtNascimento)
        {
            Id = id;
            Nome = nome;
            DtNascimento = dtNascimento;
        }
    }
}
