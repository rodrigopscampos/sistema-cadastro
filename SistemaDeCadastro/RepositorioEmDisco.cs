using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeCadastro
{
    public static class RepositorioEmDisco
    {
        const string DatabaseNome = "database.db";
        static int _id;

        static RepositorioEmDisco()
        {
            if (!File.Exists(DatabaseNome))
            {
                var stream = File.Create(DatabaseNome);
                stream.Dispose();
            }

            var todos = ListarTodos();
            if (todos.Any())
            {
                _id = todos.Max(item => item.Id);
            }
            else
            {
                _id = 0;
            }
        }

        public static void Cadastrar(string nome, DateTime dtNascimento)
        {
            _id++;
            var c = new Cliente(_id, nome, dtNascimento);

            using (var sw = new StreamWriter(DatabaseNome, append: true))
            {
                sw.WriteLine(Serializar(c));
            }
        }

        public static IEnumerable<Cliente> ListarTodos()
        {
            using (var sr = new StreamReader(DatabaseNome))
            {
                var resultado = new List<Cliente>();
                while (!sr.EndOfStream)
                {
                    var linha = sr.ReadLine();
                    var c = Desserializar(linha);
                    resultado.Add(c);
                }

                return resultado;
            }
        }

        public static Cliente Consultar(int codigoUsuario)
        {
            var todos = ListarTodos();
            return todos.FirstOrDefault(item => item.Id == codigoUsuario);
        }

        public static bool Excluir(int usuario)
        {
            var todos = ListarTodos().ToList();
            var item = Consultar(usuario);
            var sucesso = todos.Remove(item);

            if (!sucesso)
            {
                return false;
            }

            using (var sw = new StreamWriter(DatabaseNome, append: false))
            {
                foreach (var cliente in todos)
                {
                    sw.WriteLine(Serializar(cliente));
                }
            }

            return true;
        }

        private static string Serializar(Cliente c)
        {
            return $"{c.Id};{c.Nome};{c.DtNascimento}";
        }

        private static Cliente Desserializar(string texto)
        {
            var campos = texto.Split(';');

            var id = int.Parse(campos[0]);
            var nome = campos[1];
            var dtNascimento = DateTime.Parse(campos[2]);

            return new Cliente(id, nome, dtNascimento);
        }
    }
}
