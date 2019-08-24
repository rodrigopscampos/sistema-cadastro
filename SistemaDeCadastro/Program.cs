using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeCadastro
{
    class Program
    {
        static void PrintMenu()
        {
            Console.WriteLine("\n***********************\n");
            Console.WriteLine(" 1 - Listar Todos");
            Console.WriteLine(" 2 - Adicionar");
            Console.WriteLine(" 3 - Consultar");
            Console.WriteLine(" 4 - Excluir\n");
            Console.WriteLine("***********************");
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.ResetColor();
                PrintMenu();
                int opc = LerOpcao();

                if (opc == 1)
                {
                    Console.Clear();
                    ListarTodos();
                }
                else if (opc == 2)
                {
                    Console.Clear();
                    Adicionar();
                }
                else if (opc == 3)
                {
                    Console.Clear();
                    Consultar();
                }
                else if (opc == 4)
                {
                    Console.Clear();
                    Excluir();
                }
                else
                {
                    Console.Clear();
                    OpcaoInvalida();
                }
            }
        }

        static void OpcaoInvalida()
        {
            Console.WriteLine("Opção não encontrada.");
        }


        static int LerOpcao()
        {
            int opc;
            while (!int.TryParse(Console.ReadLine(), out opc))
            {
                Console.WriteLine("Entrada inválida");
            }

            return opc;
        }

        static void ListarTodos()
        {
            Console.Clear();
            var clientes = Repositorio.ListarTodos();

            if (clientes.Any())
            {
                PrintListaClienteCabecalho();

                foreach (var c in clientes)
                {
                    PrintCliente(c);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nNenhum cliente cadastrado.");
            }
        }

        static void Adicionar()
        {
            Console.Write("\nInforme o Nome do Cliente: ");
            var nome = Console.ReadLine();

            Console.Clear();
            Console.Write("\nInforme a data de nascimento (dd/mm/aaaa): ");
            var nascimentoStr = Console.ReadLine();

            DateTime dtNascimento;
            var sucesso = DateTime.TryParseExact(
                nascimentoStr,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dtNascimento);

            if (!sucesso)
            {
                Console.Clear();
                Console.WriteLine("Data de nascimento inválida, tente novamente");
                return;
            }

            Console.Clear();
            Repositorio.Cadastrar(nome, dtNascimento);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nUsuário cadastrado com sucesso!");
        }

        static void Consultar()
        {
            Console.Write("Informe o código do usuário:");
            var codigoUsuario = int.Parse(Console.ReadLine());
            var cliente = Repositorio.Consultar(codigoUsuario);

            if (cliente != null)
            {
                PrintListaClienteCabecalho();
                PrintCliente(cliente);
            }
            else
            {
                Console.WriteLine("Usuário não encontrado");
            }
        }

        static void Excluir()
        {
            Console.Write("Informe o código do usuário:");
            var codigoUsuario = int.Parse(Console.ReadLine());
            if (Repositorio.Excluir(codigoUsuario))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\nCliente removido com sucesso!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nOcorreu um erro ao remover o Cliente.");
            }
        }

        static void PrintListaClienteCabecalho()
        {
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine(string.Format("{0,-4}{1,-50}{2}", "ID", "Cliente", "Dt. Nascimento"));
        }

        static void PrintCliente(Cliente c)
        {
            string dataFormatada = c.DtNascimento.ToString("dd/MM/yyyy");
            Console.WriteLine($"{c.Id,-4}{c.Nome,-50}{dataFormatada}");
        }
    }
}
