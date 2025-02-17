using System.Runtime.CompilerServices;
using WebAppCliente.Models;

namespace WebAppCliente.Services.Interfaces
{
    public interface IAutenticacao
    {
        Task<TokenViewModel> AutenticaUsuario(UsuarioViewModel usuarioViewModel);
    }
}
