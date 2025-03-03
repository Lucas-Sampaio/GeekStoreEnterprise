﻿using GeekStore.Core.Comunication;

namespace GeekStore.WebApp.MVC.Models
{
    public class UsuarioRespostaLogin
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public double ExpireIn { get; set; }
        public UsuarioToken UsuarioToken { get; set; }
        public ResponseResult ErroResult { get; set; }
    }
}
