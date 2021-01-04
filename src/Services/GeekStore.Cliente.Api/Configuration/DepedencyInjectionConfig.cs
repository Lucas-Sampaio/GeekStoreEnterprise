using FluentValidation.Results;
using GeekStore.Clientes.Api.Application.Commands;
using GeekStore.Clientes.Api.Application.Events;
using GeekStore.Clientes.Api.Data;
using GeekStore.Clientes.Api.Data.Repository;
using GeekStore.Clientes.Api.Models;
using GeekStore.Core.Mediator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Clientes.Api.Configuration
{
    public static class DepedencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegistrarClienteCommand, ValidationResult>, ClienteCommandHandler>();
            services.AddScoped<INotificationHandler<ClienteRegistradoEvent>, ClienteEventHandler>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ClientesContext>();
        }
    }
}
