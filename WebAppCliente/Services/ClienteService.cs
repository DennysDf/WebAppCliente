using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WebAppCliente.Models;
using WebAppCliente.Services.Interfaces;

namespace WebAppCliente.Services
{
    public class ClienteService : IClienteService
    {
        private const string apiEndpoint = "/objects/";
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _clientFactory;
        private IEnumerable<ClienteViewModel> clientesViewModel;
        public ClienteService(IHttpClientFactory clientFactory)
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<ClienteViewModel>> GetClientes(string token)
        {
            var client = _clientFactory.CreateClient("DeviceAPI");
            PutTokenInHeaderAuthorization(token, client);
            using (var response = await client.GetAsync($"{apiEndpoint}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    clientesViewModel = await JsonSerializer
                                   .DeserializeAsync<IEnumerable<ClienteViewModel>>
                                   (apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return clientesViewModel;
        }
        public async Task<ClienteViewModel> GetClientePorId(int id, string token)
        {
            //    var client = _clientFactory.CreateClient("ClienteAPI");
            //    PutTokenInHeaderAuthorization(token, client);
            //    using (var response = await client.GetAsync($"{apiEndpoint}/Obter/{id}"))
            //    {
            //        if (response.IsSuccessStatusCode)
            //        {
            //            var apiResponse = await response.Content.ReadAsStreamAsync();
            //            clienteViewModel = await JsonSerializer
            //                          .DeserializeAsync<ClienteViewModel>
            //                          (apiResponse, _options);

            //        }
            //        else
            //        {
            //            return null;
            //        }
            //    }
            return null;
        }
        public async Task<ClienteViewModel> CriaCliente(ClienteViewModel ClienteVM, string token)
        {
            var client = _clientFactory.CreateClient("ClienteAPI");
            PutTokenInHeaderAuthorization(token, client);

            var Cliente = JsonSerializer.Serialize(ClienteVM);
            StringContent content = new StringContent(Cliente, Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync($"{apiEndpoint}/Cadastrar", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    ClienteVM = await JsonSerializer
                                 .DeserializeAsync<ClienteViewModel>
                                 (apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return ClienteVM;
        }
        public async Task<bool> AtualizaCliente(int id, ClienteViewModel ClienteVM, string token)
        {
            var client = _clientFactory.CreateClient("ClienteAPI");
            PutTokenInHeaderAuthorization(token, client);

            using (var response = await client.PutAsJsonAsync($"{apiEndpoint}/Atualizar/{id}", ClienteVM))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<bool> DeletaCliente(int id, string token)
        {
            var client = _clientFactory.CreateClient("ClienteAPI");
            PutTokenInHeaderAuthorization(token, client);

            using (var response = await client.DeleteAsync($"{apiEndpoint}/Deletar/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }
        private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
