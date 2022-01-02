using Microsoft.Extensions.Options;
using System.Net;
using WhiteBlackListIpControl.Models;

namespace WhiteBlackListIpControl.Middlewares
{
    public class IpSafeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IpList _ipList;
        public IpSafeMiddleware(RequestDelegate next, IOptions<IpList> options)
        {
            _next = next;
            _ipList = options.Value;
        }

        public async Task Invoke(HttpContext httpContext) // Gelen istek mutlaka Invoke metoduna duser
        {
            var requestIpAdress = httpContext.Connection.RemoteIpAddress;
            var isWhiteList = _ipList.WhiteList.Any(x => IPAddress.Parse(x).Equals(requestIpAdress));
            if (!isWhiteList)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            await _next(httpContext).ConfigureAwait(false); //Gelen istek bir sonraki middleware'e gider
        }
    }
}
