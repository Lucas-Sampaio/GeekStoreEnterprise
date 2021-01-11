using Microsoft.Extensions.DependencyInjection;
using System;

namespace GeekStore.MessageBus
{
    public static class DependenceyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection)
        {
            if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException();
            services.AddSingleton<IMessageBus>(new MessageBus(connection));
            return services;
        }
    }
}
