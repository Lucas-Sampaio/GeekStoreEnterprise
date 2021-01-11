﻿using GeekStore.Clientes.Api.Service;
using GeekStore.Core.Utils;
using GeekStore.MessageBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Clientes.Api.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddMessageBus(config.GetMessageQueueConnection("MessageBus"))
                     .AddHostedService<RegistroClienteIntegrationHandler>();
        }
    }
}
