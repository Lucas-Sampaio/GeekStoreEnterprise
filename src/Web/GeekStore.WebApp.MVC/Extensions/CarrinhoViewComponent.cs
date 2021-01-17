﻿using GeekStore.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GeekStore.WebApp.MVC.Extensions
{
    public class CarrinhoViewComponent : ViewComponent
    {
        private readonly ICarrinhoService _carrinhoService;
        public CarrinhoViewComponent(ICarrinhoService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _carrinhoService.ObterCarrinho() ?? new Models.CarrinhoVM());
        }
    }
}
