using WebAppCliente.Models;

namespace WebAppCliente.Services.Interfaces
{
    public interface  IDeviceService
    {
        Task<IEnumerable<DeviceViewModel>> GetAll();
    }
}
