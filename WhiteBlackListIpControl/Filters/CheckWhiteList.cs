using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Net;
using WhiteBlackListIpControl.Models;

namespace WhiteBlackListIpControl.Filters
{
    public class CheckWhiteList : ActionFilterAttribute
    {
        private readonly IpList _ipList;
        public CheckWhiteList(IOptions<IpList> options)
        {
            _ipList = options.Value;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var requestIpAdress = context.HttpContext.Connection.RemoteIpAddress;
            var isWhiteList = _ipList.WhiteList.Any(x => IPAddress.Parse(x).Equals(requestIpAdress));
            if (!isWhiteList)
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
