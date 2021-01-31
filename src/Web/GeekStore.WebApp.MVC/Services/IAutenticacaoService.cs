using System.Threading.Tasks;
using GeekStore.WebApp.MVC.Models;

namespace GeekStore.WebApp.MVC.Services
{
    public interface IAutenticacaoService
    {
        Task<UsuarioRespostaLogin> Login(UsuarioLoginVM usuarioLogin);
        Task<UsuarioRespostaLogin> Registro(UsuarioRegistroVM usuarioRegistro);
        Task RealizarLogin(UsuarioRespostaLogin resposta);
        Task Logout();

        bool TokenExpirado();

        Task<bool> RefreshTokenValido();
    }
}
