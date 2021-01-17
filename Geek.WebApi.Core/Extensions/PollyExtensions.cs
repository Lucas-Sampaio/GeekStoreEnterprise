using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Geek.WebApi.Core.Extensions
{
    public class PollyExtensions
    {    
            public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentar()
            {
                //polly faz a aplicação tentar de novo caso ocorra erro
                var retryWaitPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                    .WaitAndRetryAsync(new[]
                    {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10)
                    }, (outcome, timespan, retryCount, context) =>
                    {
                    //faz alguma logica quando da erro
                    Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Tentando pela {retryCount} vez!");
                        Console.ForegroundColor = ConsoleColor.White;

                    });
                return retryWaitPolicy;
            }     
    }
}
