using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeCadastro.WinFormsApp.DTO
{
    public class InputCliente
    {
        public int id { get; set; }
        public string nome { get; set; }
        public DateTime dt_nascimento { get; set; }
    }
}
