using System.Text.Json;
using WebAppCliente.Models;
using WebAppCliente.Services.Interfaces;

namespace WebAppCliente.Services
{
    public class DeviceService : IDeviceService
    {
        private const string apiEndpoint = "objects";
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _clientFactory;
        public IEnumerable<DeviceViewModel> devicesViewModel { get; set; }

        public DeviceService(IHttpClientFactory clientFactory)
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<DeviceViewModel>> GetAll()
        {
            var client = _clientFactory.CreateClient("DeviceAPI");
            using var response = await client.GetAsync(apiEndpoint);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Falha na requisição: {response.StatusCode}\nResposta: {errorContent}");
            }

            var rawJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<DeviceViewModel>>(rawJson, _options);
        }


    }
}
