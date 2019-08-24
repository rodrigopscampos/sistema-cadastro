using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeCadastro
{
    public static class Repositorio
    {
        private static int _chave = 0;
        private static Dictionary<int, Cliente> _clientes = new Dictionary<int, Cliente>();

        static Repositorio()
        {
            Cadastrar("Rodrigo Campos", new DateTime(2010, 08, 10));
            Cadastrar("João da Silva", new DateTime(2010, 08, 10));
            Cadastrar("Lucas da Silva", new DateTime(2010, 08, 10));
        }

        public static void Cadastrar(string nome, DateTime dtNascimento)
        {
            _chave++;
            var c = new Cliente(_chave, nome, dtNascimento);
            _clientes.Add(_chave, c);
        }

        public static IEnumerable<Cliente> ListarTodos()
        {
            return _clientes.Values.ToArray();
        }

        public static Cliente Consultar(int codigoUsuario)
        {
            if (_clientes.ContainsKey(codigoUsuario))
            {
                return _clientes[codigoUsuario];
            }
            else
            {
                return null;
            }
        }

        public static bool Excluir(int usuario)
        {
            if(_clientes.ContainsKey(usuario))
            {
                _clientes.Remove(usuario);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
