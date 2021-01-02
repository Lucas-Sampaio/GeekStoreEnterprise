﻿using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekStore.WebApp.MVC.Extensions
{
    public static class RazorHelpers
    {
        public static string MensagemEstoque (this RazorPage page, int quantidade)
        {
            return quantidade > 0 ? $"Apenas {quantidade} em estoque!" : "Produto esgotado";
        }
        public static string FormatoMoeda(this RazorPage page, decimal valor)
        {
            return valor > 0 ? valor.ToString("C") : "Gratuito";
        }
        public static string HashEmailForGravatar(this RazorPage page, string email)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));
            var sb = new StringBuilder();
            foreach (var item in data)
            {
                sb.Append(item.ToString("x2"));

            }
            return sb.ToString();
        }
    }
}
