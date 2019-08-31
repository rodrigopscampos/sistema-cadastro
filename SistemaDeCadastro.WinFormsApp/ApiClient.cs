using Newtonsoft.Json;
using SistemaDeCadastro.WinFormsApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeCadastro.WinFormsApp
{
    public static class ApiClient
    {
        static HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:5000")
        };

        public static void Cadastrar(string nome, DateTime dtNascimento)
        {
            var output = new OutputCliente()
            {
                nome = nome,
                dt_nascimento = dtNascimento
            };

            var json = JsonConvert.SerializeObject(output);

            var response = _httpClient.PostAsync(
                "api/usuarios", 
                new StringContent(json, Encoding.UTF8, "application/json"))
                .Result;

            response.EnsureSuccessStatusCode();
        }

        public static IEnumerable<InputCliente> ListarTodos()
        {
            var response = _httpClient.GetAsync("api/usuarios").Result;
            response.EnsureSuccessStatusCode();

            var bodyString = response.Content.ReadAsStringAsync().Result;

            var clientes = JsonConvert.DeserializeObject<InputCliente[]>(bodyString);

            return clientes;
        }

        public static void Excluir(object id)
        {
            var response = _httpClient.DeleteAsync($"api/usuarios/{id}").Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
