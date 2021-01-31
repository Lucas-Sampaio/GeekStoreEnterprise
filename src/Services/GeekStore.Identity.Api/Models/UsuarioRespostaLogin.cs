using System;

namespace GeekStore.Identity.Api.Models
{
    public class UsuarioRespostaLogin
    {
        public string AccessToken { get; set; }
        public UsuarioToken UsuarioToken { get; set; }
        public Guid RefreshToken { get;  set; }
        public double ExpiresIn { get;  set; }
    }
}
