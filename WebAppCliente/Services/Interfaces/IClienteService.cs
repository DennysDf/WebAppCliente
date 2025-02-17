using WebAppCliente.Models;

namespace WebAppCliente.Services.Interfaces
{
    public interface IClienteService
    {       

        Task<IEnumerable<ClienteViewModel>> GetClientes(string token);
        Task<ClienteViewModel> GetClientePorId(int id, string token);
        Task<ClienteViewModel> CriaCliente(ClienteViewModel ClienteVM, string token);
        Task<bool> AtualizaCliente(int id, ClienteViewModel ClienteVM, string token);
        Task<bool> DeletaCliente(int id, string token);

    }
}
