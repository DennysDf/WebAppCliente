using System.Text;
using System.Text.Json;
using WebAppCliente.Models;
using WebAppCliente.Services.Interfaces;

namespace WebAppCliente.Services
{
    public class Autenticacao : IAutenticacao
    {
        private const string apiEndpoint = "/Auth/login";
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _clientFactory;
        private TokenViewModel tokenUsuario;
        public Autenticacao(IHttpClientFactory clientFactory)
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; 
            _clientFactory = clientFactory;
        }


        public async Task<TokenViewModel> AutenticaUsuario(UsuarioViewModel usuarioVM)
        {
            var client = _clientFactory.CreateClient("ClienteAPI");
            var usuario = JsonSerializer.Serialize(usuarioVM);
            StringContent content = new StringContent(usuario, Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    tokenUsuario = await JsonSerializer
                                  .DeserializeAsync<TokenViewModel>
                                  (apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return tokenUsuario;
        }
    }
}
