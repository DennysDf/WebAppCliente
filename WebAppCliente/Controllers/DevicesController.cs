using Microsoft.AspNetCore.Mvc;
using WebAppCliente.Helper;
using WebAppCliente.Models;
using WebAppCliente.Services.Interfaces;

namespace WebAppCliente.Controllers
{
    public class DevicesController : Controller
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }   

        public async Task<IActionResult> Index()
        {
            try
            {  
                var devices = await _deviceService.GetAll();
                var iphones = devices.ToList().Select(c => new DeviceViewModel {  Id = c.Id, Name = c.Name }).Where(c => c.Name.Contains("APPLE", StringComparison.OrdinalIgnoreCase));

                var csvBytes = CsvGenerator.GenerateDevicesCsv(iphones);

                return File(
                    csvBytes,
                    "text/csv",
                    $"dispositivos_{DateTime.Now:yyyyMMddHHmmss}.csv"
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro na geração do CSV: {ex.Message}");
            }
        }
    }
}
