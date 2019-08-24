using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeCadastro.WinFormsApp
{
    public partial class FormNovoCadastro : Form
    {
        public FormNovoCadastro()
        {
            InitializeComponent();
        }

        public FormNovoCadastro(Cliente c)
            : this()
        {
            txtNome.Text = c.Nome;
            dtpDtNascimento.Value = c.DtNascimento;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            DateTime dtNascimento = dtpDtNascimento.Value;

            RepositorioEmDisco.Cadastrar(nome, dtNascimento);

            MessageBox.Show(
                "Cadatrado com sucesso",
                "Cadatro",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            this.Close();
        }
    }
}
