using System.Net;
using System.Net.Http;
using GeekStore.WebApp.MVC.Extensions;

namespace GeekStore.WebApp.MVC.Services
{
    public abstract class ServiceBase
    {
        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.NotFound:
                case HttpStatusCode.InternalServerError:
                    throw new CustomHttpRequestException(response.StatusCode);
                case HttpStatusCode.BadRequest:
                    return false;
                 
            }

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
