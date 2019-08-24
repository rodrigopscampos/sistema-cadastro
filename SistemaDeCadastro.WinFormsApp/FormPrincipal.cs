using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SistemaDeCadastro.WinFormsApp
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formNovoCadastro = new FormNovoCadastro();
            formNovoCadastro.ShowDialog(this);
            grdClientes.DataSource = RepositorioEmDisco.ListarTodos();
        }


        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            grdClientes.DataSource = RepositorioEmDisco.ListarTodos();
        }

        private void btnListarClientes_Click(object sender, EventArgs e)
        {
            grdClientes.DataSource = RepositorioEmDisco.ListarTodos();
        }

        private void grdClientes_CellMouseClick(
            object sender,
            DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            var cliente = (Cliente)grdClientes.Rows[e.RowIndex].DataBoundItem;


            var menuExcluir = new MenuItem("Excluir");
            menuExcluir.Click += (_, __) =>
            {
                RepositorioEmDisco.Excluir(cliente.Id);
                grdClientes.DataSource = RepositorioEmDisco.ListarTodos();
            };

            var menuEditar = new MenuItem("Editar");
            menuEditar.Click += (_, __) =>
            {
                RepositorioEmDisco.Excluir(cliente.Id);
                var form = new FormNovoCadastro(cliente);
                form.ShowDialog();
                grdClientes.DataSource = RepositorioEmDisco.ListarTodos();
            };

            var menu = new ContextMenu();
            menu.MenuItems.Add(menuExcluir);
            menu.MenuItems.Add(menuEditar);

            menu.Show(grdClientes, e.Location);
        }

        private void btnExportarCsv_Click(object sender, EventArgs e)
        {
            var dados = (IEnumerable<Cliente>)grdClientes.DataSource;
            var csvLinhas = new List<string>();

            csvLinhas.Add("ID;Nome;Dt. Nascimento");
            foreach (Cliente item in dados)
            {
                csvLinhas.Add($"{item.Id};{item.Nome};{item.DtNascimento}");
            }

            var dialog = new SaveFileDialog();
            var resultado = dialog.ShowDialog(this);

            if (resultado == DialogResult.OK)
            {
                var nomeArquivo = dialog.FileName;

                using (var sw = new StreamWriter(nomeArquivo))
                {
                    foreach (var linha in csvLinhas)
                    {
                        sw.WriteLine(linha);
                    }
                }
            }
        }
    }
}
