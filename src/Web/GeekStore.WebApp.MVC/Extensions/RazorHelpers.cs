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
            return valor > 0 ?  FormatoMoeda(valor) : "Gratuito";
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

        public static string UnidadesPorProduto(this RazorPage page, int unidades)
        {
            return unidades > 1 ? $"{unidades} unidades" : $"{unidades} unidade";
        }
        public static string SelectOptionsPorQuantidade(this RazorPage page, int quantidade, int valorSelecionado = 0)
        {
            var sb = new StringBuilder();
            for (int i = 0; i <= quantidade; i++)
            {
                var selected = "";
                if (i == valorSelecionado) selected = "selected";
                sb.Append($"<option {selected} value='{i}'> {i} </option>");
            }
            return sb.ToString();
        }
        public static string UnidadesPorProdutoValorTotal(this RazorPage page, int unidades, decimal valor)
        {
            return $"{unidades}x {FormatoMoeda(valor)} = Total: {FormatoMoeda(valor * unidades)}";
        }

        public static string ExibeStatus(this RazorPage page, int status)
        {
            var statusMensagem = "";
            var statusClasse = "";

            switch (status)
            {
                case 1:
                    statusClasse = "info";
                    statusMensagem = "Em aprovação";
                    break;
                case 2:
                    statusClasse = "primary";
                    statusMensagem = "Aprovado";
                    break;
                case 3:
                    statusClasse = "danger";
                    statusMensagem = "Recusado";
                    break;
                case 4:
                    statusClasse = "success";
                    statusMensagem = "Entregue";
                    break;
                case 5:
                    statusClasse = "warning";
                    statusMensagem = "Cancelado";
                    break;

            }

            return $"<span class='badge badge-{statusClasse}'>{statusMensagem}</span>";
        }
        private static string FormatoMoeda(decimal valor)
        {
            return valor.ToString("C");
        }
    }
}
