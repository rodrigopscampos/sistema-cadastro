using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeCadatro.API.DTO
{
    public class OutputCliente
    {
        public int id { get; set; }
        public string nome { get; set; }
        public DateTime dt_nascimento { get; set; }
    }
}
