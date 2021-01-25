using FluentValidation.Results;
using Geek.WebApi.Core.Usuario;
using GeekStore.Core.Mediator;
using GeekStore.Pedido.Api.Application.Commands;
using GeekStore.Pedido.Api.Application.Events;
using GeekStore.Pedido.Api.Application.Queries;
using GeekStore.Pedidos.Domain.Pedidos;
using GeekStore.Pedidos.Domain.Vouchers;
using GeekStore.Pedidos.Infra;
using GeekStore.Pedidos.Infra.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Pedido.Api.Configuration
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //api
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            //application
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IVoucherQueries, VoucherQueries>();
            services.AddScoped<IPedidoQueries, PedidoQueries>();

            //command
            services.AddScoped<IRequestHandler<RealizarPedidoCommand, ValidationResult>, PedidoCommandHandler>();
            //events
            services.AddScoped<INotificationHandler<PedidoRealizadoEvent>, PedidoEventHandler>();
            //data
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<PedidosContext>();
        }
    }
}
