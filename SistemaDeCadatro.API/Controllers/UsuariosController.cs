using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro;
using SistemaDeCadatro.API.DTO;

namespace SistemaDeCadatro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public void Post([FromBody] InputCadastro input)
        {
            RepositorioEmDisco.Cadastrar(input.Nome, input.Dt_Nascimento);
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<OutputCliente> ListarUsuarios()
        {
            return RepositorioEmDisco.ListarTodos()
                .Select(c => new OutputCliente
                {
                    id = c.Id,
                    nome = c.Nome,
                    dt_nascimento = c.DtNascimento
                });
        }

        [HttpGet]
        [Route("{id}")]
        public OutputCliente GetUsuario(int id)
        {
            var c = RepositorioEmDisco.Consultar(id);

            return new OutputCliente()
            {
                id = c.Id,
                nome = c.Nome,
                dt_nascimento = c.DtNascimento
            };
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            if(RepositorioEmDisco.Excluir(id))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}